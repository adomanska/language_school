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
        private StudentDAL studentDAL = new StudentDAL();
        private Regex firstNameRegex;
        private Regex lastNameRegex;
        private Regex emailRegex;
        private Regex phoneNumberRegex;

        public StudentBLL()
        {
            firstNameRegex = new Regex(@"[A-Z][a-z]*");
            lastNameRegex = new Regex(@"([A-Z][a-z]*)(-[A-Z][a-z]*)*");
            emailRegex = new Regex(@"\A(?:[a-z0-9!#$%&'*+/=?^_`{|}~-]+(?:\.[a-z0-9!#$%&'*+/=?^_`{|}~-]+)*@(?:[a-z0-9](?:[a-z0-9-]*[a-z0-9])?\.)+[a-z0-9](?:[a-z0-9-]*[a-z0-9])?)\Z", RegexOptions.IgnoreCase);
            phoneNumberRegex = new Regex(@"/^(?:\(?\+?48)?(?:[-\.\(\)\s]*(\d)){9}\)?$/");
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
            if (!firstNameRegex.IsMatch(firstName))
                throw new Exception("Invalid First Name");
            if (!lastNameRegex.IsMatch(lastName))
                throw new Exception("Invalid Last Name");
            if (!emailRegex.IsMatch(email))
                throw new Exception("Invalid Email Address");
            if (phoneNumber != "" && !phoneNumberRegex.IsMatch(phoneNumber))
                throw new Exception("Invalid Phone Number");
            Student student = new Student { FirstName = firstName, LastName = lastName, Email = email, PhoneNumber = phoneNumber };
            try
            {
                studentDAL.Add(student);
            }
            catch
            {
                throw;
            }
        }
    }
}
