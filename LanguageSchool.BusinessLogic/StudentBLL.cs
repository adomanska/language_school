using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LanguageSchool.DataAccess;
using LanguageSchool.Model;
using System.Text.RegularExpressions;
using System.Data.Entity;
using LanguageSchool.Presentation;

namespace LanguageSchool.BusinessLogic
{
    public class StudentBLL: IStudentBLL
    {
        private IStudentDAL studentDAL;
        private Regex firstNameRegex;
        private Regex lastNameRegex;
        private Regex emailRegex;
        private Regex phoneNumberRegex;

        public StudentBLL(IStudentDAL _studentDAL)
        {
            studentDAL = _studentDAL;
            firstNameRegex = new Regex(@"^[A-Z][a-z]+");
            lastNameRegex = new Regex(@"^([A-Z][a-z]*)(-[A-Z][a-z]+)*");
            emailRegex = new Regex(@"\A(?:[a-z0-9!#$%&'*+/=?^_`{|}~-]+(?:\.[a-z0-9!#$%&'*+/=?^_`{|}~-]+)*@(?:[a-z0-9](?:[a-z0-9-]*[a-z0-9])?\.)+[a-z0-9](?:[a-z0-9-]*[a-z0-9])?)\Z", RegexOptions.IgnoreCase);
            phoneNumberRegex = new Regex(@"^((\+\d{1,3}(-| )?\(?\d\)?(-| )?\d{1,5})|(\(?\d{2,6}\)?))(-| )?(\d{3,4})(-| )?(\d{4})(( x| ext)\d{1,5}){0,1}$");
        }

        private bool IsValidData(string firstName, string lastName, string email, string phoneNumber = "")
        {
            string error = null;
            if (!Validator.IsFirstNameValid(firstName, ref error))
                throw new Exception(error);
            if (!Validator.IsLastNameValid(lastName, ref error))
                throw new Exception(error);
            if (!Validator.IsEmailValid(email, ref error))
                throw new Exception(error);
            if (phoneNumber != null && phoneNumber != "" && !Validator.IsPhoneNumberValid(phoneNumber, ref error))
                throw new Exception(error);

            return true;
        }
        public List<Student> GetAll()
        {
            try
            {
                return studentDAL.GetAll().ToList();
            }
            catch
            {
                throw;
            }
        }
        public void Add(string firstName, string lastName, string email, string phoneNumber="")
        {
            try
            {
                Student existingStudent = studentDAL.FindByEmail(email);
                if (existingStudent != null)
                    throw new Exception("Student with such email already exists");

                IsValidData(firstName, lastName, email, phoneNumber);

                Student student = new Student {
                    FirstName = firstName,
                    LastName = lastName,
                    Email = email,
                    PhoneNumber = phoneNumber == "" ? null : phoneNumber
                };

                studentDAL.Add(student);
            }
            catch
            {
                throw;
            }
        }

        public void SignForClass(Student student, Class languageClass)
        {
            studentDAL.SignForClass(student, languageClass);
        }
        

        public void Update(int id, string firstName, string lastName, string email, string phoneNumber = "")
        {
            try
            {
                Student existingStudent = studentDAL.FindByEmail(email);
                if (existingStudent != null && existingStudent.ID != id)
                    throw new Exception("Student with such email already exists");

                IsValidData(firstName, lastName, email, phoneNumber);
                studentDAL.Update(id, firstName, lastName, email, phoneNumber == "" ? null : phoneNumber);
            }
            catch
            {
                throw;
            }
        }

        public (List<Student> students, int pageCount) Search(StudentFilter filter)
        {
            var query = studentDAL.Search(filter.Filter, filter.Text, filter.IsSorted);
            var count = Math.Ceiling(((double)query.Count()) / filter.PageSize);
            var list = query.Skip(filter.PageSize * (filter.PageNumber - 1)).Take(filter.PageSize).ToList();

            return (list, (int)count);
        }
    }
}
