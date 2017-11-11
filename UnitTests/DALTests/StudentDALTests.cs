using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LanguageSchool.DataAccess;
using NUnit.Framework;
using LanguageSchool.Model;

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
        public void GetAll_Always_ReturnAllStudents()
        {
            var result = studentDAL.GetAll();
            Assert.That(result.Count, Is.EqualTo(context.Students.Count()));
        }

        [Test]
        public void GetAll_Always_ReturnsCorrectEmailOfFirstStudent()
        {
            var result = studentDAL.GetAll();
            var email = result.First().Email;
            Assert.That(email, Is.EqualTo("kate@gmail.com"));
        }

        [Test]
        public void SearchByName_WhenNameExists_ReturnsCorrectStudent()
        {
            var result = studentDAL.Search(SearchBy.LastName, "Davis", false);
            Assert.That(result.First().ID, Is.EqualTo(5));
        }

        [Test]
        public void SearchByEmail_WhenEmailExists_ReturnsCorrectStudent()
        {
            var result = studentDAL.Search(SearchBy.Email, "king@gmail.com", false);
            Assert.That(result.First().ID, Is.EqualTo(4));
        }

        [Test]
        public void SearchByName_WhenIsAlphabeticallySorted_ReturnsCorrectFirstStudent()
        {
            var result = studentDAL.Search(SearchBy.LastName, "", true);
            Assert.That(result.First().ID, Is.EqualTo(2));
        }

        [Test]
        public void SearchByEmail_WhenIsAlphabeticallySorted_ReturnsCorrectFirstStudent()
        {
            var result = studentDAL.Search(SearchBy.Email, "", true);
            Assert.That(result.First().ID, Is.EqualTo(5));
        }

        [Test]
        public void Update_NonExisitingStudent_ThrowsException()
        {
            Assert.Throws<Exception>(() => studentDAL.Update(-1, "Tom", "Cruise", "tom@gmail.com", "503998452"));
        }

        [Test]
        public void Update_ExisitingStudent_ReturnCorrectResult()
        {
            studentDAL.Update(2, "John", "Cruise", "tom@gmail.com", "503998452");
            var firstName = context.Students.Where(x => x.ID == 2).First().FirstName;
            Assert.That(firstName, Is.EqualTo("John"));
        }

        [Test]
        public void FindByEmail_NonExistingEmail_ReturnsNull()
        {
            var result = studentDAL.FindByEmail("example@gmail.com");
            Assert.IsNull(result);
        }

        [TestCase("kate@gmail.com", 1)]
        [TestCase("elizabeth@gmail.com", 3)]
        public void FindByEmail_ExistingEmail_ReturnsCorrectStudent(string email, int id)
        {
            var result = studentDAL.FindByEmail(email);
            Assert.IsNotNull(result);
            Assert.That(result.ID, Is.EqualTo(id));
        }

        [Test]
        public void FindByID_NonExistingID_ReturnsNull()
        {
            var result = studentDAL.FindByID(-1);
            Assert.IsNull(result);
        }

        [TestCase(1)]
        [TestCase(3)]
        public void FindByID_ExistingID_ReturnsCorrectStudent(int id)
        {
            var result = studentDAL.FindByID(id);
            Assert.IsNotNull(result);
            Assert.That(result.ID, Is.EqualTo(id));
        }


        [Test]
        public void SignForClass_WhenStudentIsNotSigned_IncreaseStudentClassesCount()
        {
            Student s = context.Students.First();
            int countClassesBefore = s.Classes.Count;
            Class c = context.Classes.First();

            studentDAL.SignForClass(s, c);

            int countClassesAfter = s.Classes.Count;
            Assert.That(countClassesBefore + 1, Is.EqualTo(countClassesAfter));
        }

        [Test]
        public void Add_WhenNewStudentNonExists_IncreaseStudentsCount()
        {
            int count1 = context.Students.Count();
            var student = new Student
            {
                ID = 6,
                FirstName = "Paul",
                LastName = "Kingson",
                Email = "paulking@gmail.com",
                PhoneNumber = "789652314",
            };
            studentDAL.Add(student);
            
            int count2 = context.Students.Count();
            Assert.That(count1 + 1, Is.EqualTo(count2));
            context.Students.Remove(student);
        }
    }
}
