using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LanguageSchool.DataAccess;
using LanguageSchool.Model;
using System.Text.RegularExpressions;
using System.Data.Entity;

namespace LanguageSchool.BusinessLogic
{
    public class StudentBLL: IStudentBLL
    {
        private IStudentDAL studentDAL;
        private Regex firstNameRegex;
        private Regex lastNameRegex;
        private Regex emailRegex;
        private Regex phoneNumberRegex;

        public StudentBLL(LanguageSchoolContext context)
        {
            studentDAL = new StudentDAL(context);
            firstNameRegex = new Regex(@"[A-Z][a-z]*");
            lastNameRegex = new Regex(@"([A-Z][a-z]*)(-[A-Z][a-z]*)*");
            emailRegex = new Regex(@"\A(?:[a-z0-9!#$%&'*+/=?^_`{|}~-]+(?:\.[a-z0-9!#$%&'*+/=?^_`{|}~-]+)*@(?:[a-z0-9](?:[a-z0-9-]*[a-z0-9])?\.)+[a-z0-9](?:[a-z0-9-]*[a-z0-9])?)\Z", RegexOptions.IgnoreCase);
            phoneNumberRegex = new Regex(@"^((\+\d{1,3}(-| )?\(?\d\)?(-| )?\d{1,5})|(\(?\d{2,6}\)?))(-| )?(\d{3,4})(-| )?(\d{4})(( x| ext)\d{1,5}){0,1}$");
        }

        private string StandarizeInput(string input)
        {
            return input.First().ToString().ToUpper() + input.Substring(1).ToLower();
        }

        private bool IsValidData(string firstName, string lastName, string email, string phoneNumber = "")
        {
            if (!firstNameRegex.IsMatch(firstName))
                throw new Exception("Invalid First Name");
            if (!lastNameRegex.IsMatch(lastName))
                throw new Exception("Invalid Last Name");
            if (!emailRegex.IsMatch(email))
                throw new Exception("Invalid Email Address");
            if (phoneNumber != null && phoneNumber != "" && !phoneNumberRegex.IsMatch(phoneNumber))
                throw new Exception("Invalid Phone Number");

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
            IsValidData(firstName, lastName, email, phoneNumber);

            Student student = new Student { FirstName = StandarizeInput(firstName), LastName = StandarizeInput(lastName), Email = email, PhoneNumber = phoneNumber=="" ? null : phoneNumber };
            try
            {
                studentDAL.Add(student);
            }
            catch
            {
                throw;
            }
        }

        public void SignForClass(int studentID, Class languageClass)
        {
            StudentToClass studentToClass = new StudentToClass { StudentRefID = studentID, ClassRefID = languageClass.ClassID };
            try
            {
                studentDAL.SignForClass(studentToClass);
            }
            catch
            {
                throw;
            }
        }
        

        public Student FindByEmail(string email)
        {
            try
            {
                return studentDAL.FindByEmail(email);
            }
            catch
            {
                throw;
            }
        }

        public Predicate<object> GetFilterByEmailPredicate(string email)
        {
            Predicate<object> filtre = item =>
            {
                dynamic student = item;
                if (!student.Email.Contains(email))
                    return false;
                else
                    return true;
            };

            return filtre;
        }

        public Predicate<object> GetFilterByLastNamePredicate(string lastName)
        {
            Predicate<object> filtre = item =>
            {
                dynamic student = item;
                if (!student.LastName.ToLower().Contains(lastName.ToLower()))
                    return false;
                else
                    return true;
            };

            return filtre;
        }
        public IQueryable<Student> FindByLastName(string lastName)
        {
            try
            {
                return studentDAL.FindByLastName(StandarizeInput(lastName));
            }
            catch
            {
                throw;
            }
        }

        public void Update(int id, string firstName, string lastName, string email, string phoneNumber = "")
        {
            try
            {
                IsValidData(firstName, lastName, email, phoneNumber);
                studentDAL.Update(id, StandarizeInput(firstName), StandarizeInput(lastName), email, phoneNumber == "" ? null : phoneNumber);
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

    public class StudentFilter
    {
        public string Text { get; set; }
        public SearchBy Filter { get; set; }
        public bool IsSorted { get; set; }

        public int PageNumber { get; set; }
        public int PageSize { get; set; }
    }
}
