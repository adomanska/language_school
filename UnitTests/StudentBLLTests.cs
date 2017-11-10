using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LanguageSchool.BusinessLogic;
using LanguageSchool.DataAccess;
using NUnit.Framework;

namespace UnitTests
{
    [TestFixture]
    public class StudentBLLTests
    {
        LanguageSchoolMockContext context;
        StudentBLL studentBLL;

        public StudentBLLTests()
        {
            context = new LanguageSchoolMockContext();
            studentBLL = new StudentBLL(new StudentDAL(context));
        }

        [Test]
        public void GetAll_Always_ReturnAllStudents()
        {

        }

    }
}
