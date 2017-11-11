using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LanguageSchool.DataAccess;
using NUnit.Framework;

namespace UnitTests
{
    [TestFixture]
    class StudentDALTests
    {
        LanguageSchoolMockContext context;
        StudentDAL studentDAL;

        public StudentDALTests()
        {
            context = new LanguageSchoolMockContext();
            studentDAL = new StudentDAL(context);
        }

        [Test]
        public void DALGetAll_Always_ReturnAllStudents()
        {
            var result = studentDAL.GetAll();
            Assert.That(result.Count, Is.EqualTo(5));
        }

        [Test]
        public void DALGetAll_Always_ReturnsCorrectEmailOfFirstStudent()
        {
            var result = studentDAL.GetAll();
            var email = result.First().Email;
            Assert.That(email, Is.EqualTo("kate@gmail.com"));
        }

        [Test]
        public void DALSearchByName_WhenNameExists_ReturnsCorrectStudent()
        {
            var result = studentDAL.Search(SearchBy.LastName, "Brown", false);
            Assert.That(result.First().Email, Is.EqualTo("tomb@gmail.com"));
        }

        [Test]
        public void DAL
    }
}
