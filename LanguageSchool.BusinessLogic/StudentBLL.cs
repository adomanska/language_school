using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LanguageSchool.DataAccess;
using LanguageSchool.Model;
using System.Text.RegularExpressions;

namespace LanguageSchool.BusinessLogic
{
    public class StudentBLL
    {
        private StudentDAL studentDAL;
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
            phoneNumberRegex = new Regex(@"/^(?:\(?\+?48)?(?:[-\.\(\)\s]*(\d)){9}\)?$/");
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
            if (phoneNumber != "" && !phoneNumberRegex.IsMatch(phoneNumber))
                throw new Exception("Invalid Phone Number");

            return true;
        }
        public List<Student> GetAll()
        {
            try
            {
                return studentDAL.GetAll();
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

        public Student FindByLastName(string lastName)
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

        public void UpdateFirstName(string id, string firstName)
        {
            try
            {
                if (!firstNameRegex.IsMatch(firstName))
                    throw new Exception("Invalid First Name");

                studentDAL.UpdateFirstName(id, StandarizeInput(firstName));
            }
            catch
            {
                throw;
            }
        }

    }
}
