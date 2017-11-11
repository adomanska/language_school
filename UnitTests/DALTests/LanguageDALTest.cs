using LanguageSchool.DataAccess;
using LanguageSchool.Model;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UnitTests
{
    [TestFixture]
    class LanguageDALTest
    {
        private LanguageSchoolMockContext context;
        private ILanguageDAL languageDAL;
        public LanguageDALTest()
        {
            context = new LanguageSchoolMockContext();
            languageDAL = new LanguageDAL(context);
        }

        [Test]
        public void GetAll_Always_ReturnsAllLanguages()
        {
            var result = languageDAL.GetAll().Count();
            Assert.That(result, Is.EqualTo(3));
        }

        [TestCase(-1)]
        [TestCase(10)]
        public void GetById_InvalidId_ReturnsNull(int id)
        {
            var result = languageDAL.GetById(id);
            Assert.IsNull(result);
        }

        [TestCase(1)]
        [TestCase(3)]
        public void GetById_ValidId_ReturnsNull(int id)
        {
            var result = languageDAL.GetById(id);
            Assert.IsNotNull(result);
            Assert.IsInstanceOf(typeof(Language), result);
        }
    }
}
