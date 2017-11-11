using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LanguageSchool.DataAccess;

namespace UnitTests
{
    [TestFixture]
    class LanguageLevelDALTest
    {
        private LanguageSchoolMockContext context;
        private LanguageLevelDAL languageLevelDAL;

        public LanguageLevelDALTest()
        {
            context = new LanguageSchoolMockContext();
            languageLevelDAL = new LanguageLevelDAL(context);
        }

        [Test]
        public void GetAll_Always_ReturnsAllLanguageLevels()
        {
            var result = languageLevelDAL.GetAll().Count();
            Assert.That(result, Is.EqualTo(6));
        }
    }
}
