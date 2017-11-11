using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace LanguageSchool.Presentation
{
    public static class Validator
    {
        static Regex firstNameRegex = new Regex(@"^[A-Z][a-z]+$");
        static Regex lastNameRegex = new Regex(@"^([A-Z][a-z]+)(-[A-Z][a-z]+)*$");
        static Regex emailRegex = new Regex(@"\A(?:[a-z0-9!#$%&'*+/=?^_`{|}~-]+(?:\.[a-z0-9!#$%&'*+/=?^_`{|}~-]+)*@(?:[a-z0-9](?:[a-z0-9-]*[a-z0-9])?\.)+[a-z0-9](?:[a-z0-9-]*[a-z0-9])?)\Z", RegexOptions.IgnoreCase);
        static Regex phoneNumberRegex = new Regex(@"^((\+\d{1,3}(-| )?\(?\d\)?(-| )?\d{1,5})|(\(?\d{2,6}\)?))(-| )?(\d{3,4})(-| )?(\d{4})(( x| ext)\d{1,5}){0,1}$");

        public static bool IsFirstNameValid(string firstName, ref string error)
        {
            if (String.IsNullOrEmpty(firstName))
            {
                error = "First Name is required";
                return false;
            }
            else if (!firstNameRegex.IsMatch(firstName))
            {
                error = "Invalid First Name";
                return false;
            }

            error = null;
            return true;
        }

        public static bool IsLastNameValid(string lastName, ref string error)
        {
            if (String.IsNullOrEmpty(lastName))
            {
                error = "Last Name is required";
                return false;
            }
            else if (!lastNameRegex.IsMatch(lastName))
            {
                error = "Invalid Last Name";
                return false;
            }

            error = null;
            return true;
        }

        public static bool IsEmailValid(string email, ref string error)
        {
            if (String.IsNullOrEmpty(email))
            {
                error = "Email is required";
                return false;
            }
            else if (!emailRegex.IsMatch(email))
            {
                error = "Invalid Email Address";
                return false;
            }

            error = null;
            return true;
        }

        public static bool IsPhoneNumberValid(string phoneNumber, ref string error)
        {
           if (!String.IsNullOrEmpty(phoneNumber) && !phoneNumberRegex.IsMatch(phoneNumber))
            {
                error = "Invalid Phone Number";
                return false;
            }

            error = null;
            return true;
        }
    }
}
