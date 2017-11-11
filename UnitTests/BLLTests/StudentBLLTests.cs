using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LanguageSchool.BusinessLogic;
using LanguageSchool.DataAccess;
using LanguageSchool.Model;
using NUnit.Framework;
using Moq;
using System.Collections.ObjectModel;

namespace UnitTests
{
    [TestFixture]
    public class StudentBLLTests
    {
        Mock<IStudentDAL> mockStudentDAL;
        StudentBLL studentBLL;

        public StudentBLLTests()
        {
            mockStudentDAL = new Mock<IStudentDAL>();
            studentBLL = new StudentBLL(mockStudentDAL.Object);
        }

        

        [Test]
        public void GetAll_Always_ReturnAllStudents()
        {
            var mockStudentList = new List<Student>
            {
                new Student()
                {
                   ID = 1,
                   FirstName = "Kate",
                   LastName = "Smith",
                   Email = "kate@gmail.com",
                   PhoneNumber = "536987415"
                },
                new Student()
                {
                   ID = 2,
                   FirstName = "Tom",
                   LastName = "Brown",
                   Email = "tomb@gmail.com",
                   PhoneNumber = "236859714"
                },
                new Student()
                {
                   ID = 3,
                   FirstName = "Elizabeth",
                   LastName = "Jones",
                   Email = "elizabeth@gmail.com",
                   PhoneNumber = "444555236"
                }
            };

            mockStudentDAL.Setup(x => x.GetAll()).Returns(mockStudentList.AsQueryable);
            var result = studentBLL.GetAll();
            Assert.That(result.Count, Is.EqualTo(3));
        }

        [TestCase("Tom", "Watson", "tom@gmail.com", "545898452")]
        public void Add_WhenEmailExists_ThrowException(string firstName, string lastName, string email, string phoneNumber = "")
        {
            mockStudentDAL.Setup(x => x.FindByEmail(email)).Returns(
                new Student()
                {
                    ID = 2,
                    FirstName = "Tom",
                    LastName = "Brown",
                    Email = "tomb@gmail.com",
                    PhoneNumber = "236859714"
                });
            mockStudentDAL.Setup(x => x.Add(new Student()));

            Assert.Throws<Exception>(() => studentBLL.Add(firstName, lastName, email, phoneNumber));
        }

        [TestCase("Emma34", "Wats4on", "emma@gmail.com", "412563987")]
        public void Add_WhenNameIsInvalid_ThrowException(string firstName, string lastName, string email, string phoneNumber = "")
        {
            mockStudentDAL.Setup(x => x.FindByEmail(email)).Returns((Student)null);
            mockStudentDAL.Setup(x => x.Add(new Student()));

            Assert.Throws<Exception>(() => studentBLL.Add(firstName, lastName, email, phoneNumber));
        }

        [TestCase("Emma", "Watson", "emailwp.pl", "545898452")]
        public void Add_WhenEmailIsInvalid_ThrowException(string firstName, string lastName, string email, string phoneNumber = "")
        {
            mockStudentDAL.Setup(x => x.FindByEmail(email)).Returns((Student)null);
            mockStudentDAL.Setup(x => x.Add(new Student()));

            Assert.Throws<Exception>(() => studentBLL.Add(firstName, lastName, email, phoneNumber));
        }

        [Test]
        public void SignForClass_WhenStudentIsSigned_throwsException()
        {
            Class c = new Class()
            {
                ClassID = 1,
                ClassName = "English M1",
                LanguageRefID = 1,
                LanguageLevelRefID = 1,
                StartTime = "10:00",
                EndTime = "11:30",
                Day = DayOfWeek.Monday,
                Students = new Collection<Student>()
            };

            Student s = new Student()
            {
                ID = 2,
                FirstName = "Tom",
                LastName = "Brown",
                Email = "tomb@gmail.com",
                PhoneNumber = "236859714",
                Classes = new Collection<Class>()
            };

            s.Classes.Add(c);

            mockStudentDAL.Setup(x => x.FindByID(s.ID)).Returns(s);
            mockStudentDAL.Setup(x => x.SignForClass(s, c));

            Assert.Throws<Exception>(() => studentBLL.SignForClass(s.ID, c));
        }

        [TestCase(100, "Sam", "Smith", "tomb@gmail.com", "545898452")]
        public void Update_WhenNewEmailExists_ThrowException(int id, string firstName, string lastName, string email, string phoneNumber = "")
        {
            mockStudentDAL.Setup(x => x.FindByEmail(email)).Returns(
                new Student()
                {
                    ID = 2,
                    FirstName = "Tom",
                    LastName = "Brown",
                    Email = "tomb@gmail.com",
                    PhoneNumber = "236859714"
                });
            mockStudentDAL.Setup(x => x.Update(id, firstName, lastName, email, phoneNumber));

            Assert.Throws<Exception>(() => studentBLL.Update(id, firstName, lastName, email, phoneNumber));
        }

        [TestCase(100, "Sam856", "Smith565", "sam@gmail.com", "545898452")]
        public void Update_WhenNameIsInvalid_ThrowException(int id, string firstName, string lastName, string email, string phoneNumber = "")
        {
            mockStudentDAL.Setup(x => x.FindByEmail(email)).Returns((Student)null);
            mockStudentDAL.Setup(x => x.Update(id, firstName, lastName, email, phoneNumber));

            Assert.Throws<Exception>(() => studentBLL.Update(id, firstName, lastName, email, phoneNumber));
        }

        [TestCase(100, "Sam", "Smith", "samgmail.com", "545898452")]
        public void Update_WhenEmailIsInvalid_ThrowException(int id, string firstName, string lastName, string email, string phoneNumber = "")
        {
            mockStudentDAL.Setup(x => x.FindByEmail(email)).Returns((Student)null);
            mockStudentDAL.Setup(x => x.Update(id, firstName, lastName, email, phoneNumber));

            Assert.Throws<Exception>(() => studentBLL.Update(id, firstName, lastName, email, phoneNumber));
        }

        [TestCase(2, 2, 1, 2)]
        [TestCase(1, 2, 2, 2)]
        [TestCase(2, 1, 1, 3)]
        [TestCase(1, 3, 3, 1)]
        public void Search_WhenSpecifiedFilter_ReturnExpectedResult(int pageNumber, int pageSize, int resultStudentsCount, int resultPageCount)
        {
            StudentFilter filter = new StudentFilter
            {
                Filter = SearchBy.LastName,
                IsSorted = false,
                Text = "Smit",
                PageNumber = pageNumber,
                PageSize = pageSize
            };

            var mockFiltredStudentList = new List<Student>
            {
                new Student()
                {
                   ID = 1,
                   FirstName = "Kate",
                   LastName = "Smith",
                   Email = "kate@gmail.com",
                   PhoneNumber = "536987415"
                },
                new Student()
                {
                   ID = 2,
                   FirstName = "Tom",
                   LastName = "Smithway",
                   Email = "tomb@gmail.com",
                   PhoneNumber = "236859714"
                },
                new Student()
                {
                   ID = 3,
                   FirstName = "Elizabeth",
                   LastName = "Smithw",
                   Email = "elizabeth@gmail.com",
                   PhoneNumber = "444555236"
                }
            };

            mockStudentDAL.Setup(x => x.Search(filter.Filter, filter.Text, filter.IsSorted)).Returns(mockFiltredStudentList.AsQueryable);
            List<Student> students;
            int pageCount;

            (students, pageCount) = studentBLL.Search(filter);

            Assert.That(students.Count, Is.EqualTo(resultStudentsCount));
            Assert.That(pageCount, Is.EqualTo(resultPageCount));
        }

    }
}
