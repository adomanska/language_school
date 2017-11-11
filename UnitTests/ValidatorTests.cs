using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using LanguageSchool.Presentation;

namespace UnitTests
{
    [TestFixture]
    class ValidatorTests
    {
        [TestCase("Kate", true)]
        [TestCase("3Ka4te", false)]
        public void IsFirstNameValid_Always_ReturnsExpectedResult(string firstName, bool expectedResult)
        {
            string error = null;
            var result = Validator.IsFirstNameValid(firstName, ref error);

            Assert.That(result, Is.EqualTo(expectedResult));
        }

        [TestCase("Watson", true)]
        [TestCase("Wat4son", false)]
        public void IsLastNameValid_Always_ReturnsExpectedResult(string lastName, bool expectedResult)
        {
            string error = null;
            var result = Validator.IsLastNameValid(lastName, ref error);

            Assert.That(result, Is.EqualTo(expectedResult));
        }

        [TestCase("katiew@gmail.com", true)]
        [TestCase("kateiwgmail.com", false)]
        public void IsEmailValid_Always_ReturnsExpectedResult(string email, bool expectedResult)
        {
            string error = null;
            var result = Validator.IsEmailValid(email, ref error);

            Assert.That(result, Is.EqualTo(expectedResult));
        }

        [TestCase("536987412", true)]
        [TestCase("569-25a-563", false)]
        public void IsPhoneNumberValid_Always_ReturnsExpectedResult(string phoneNumber, bool expectedResult)
        {
            string error = null;
            var result = Validator.IsPhoneNumberValid(phoneNumber, ref error);

            Assert.That(result, Is.EqualTo(expectedResult));
        }
    }
}
