using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using LanguageSchool.DataAccess;
using LanguageSchool.BusinessLogic;
using LanguageSchool.Model;
using Moq;
using System.Data.Entity;

namespace UnitTests
{
    [TestFixture]
    class LanguageBLLTest
    {
        Mock<ILanguageDAL> mockLanguageDAL;
        ILanguageBLL languageBLL;
        List<Language> languages;

        public LanguageBLLTest()
        {
            mockLanguageDAL = new Mock<ILanguageDAL>();
            languages = new List<Language>()
            {
                new Language()
                {
                    LanguageID=1,
                    LanguageName = "English"
                },
                new Language()
                {
                    LanguageID=2,
                    LanguageName="Spanish"
                },
                new Language()
                {
                    LanguageID=3,
                    LanguageName="Russian"
                }
            };
            mockLanguageDAL.Setup(mr => mr.GetAll()).Returns(languages.AsQueryable());
            languageBLL = new LanguageBLL(mockLanguageDAL.Object);
        }

        [Test]
        public void GetAll_ALways_RetunsAllLanguages()
        {
            var result = languageBLL.GetAll().Count;
            Assert.That(result, Is.EqualTo(3));
        }

        [TestCase("English", true)]
        [TestCase("Italian", false)]
        public void Exists_Always_ReturnsExpectedResult(string language, bool exist)
        {
            var result = languageBLL.Exists(language);
            Assert.That(result, Is.EqualTo(exist));
        }

        [TestCase("French", true)]
        [TestCase("enlish", false)]
        public void IsValidLanguage_Always_ReturnsExpectedResult(string language, bool isValid)
        {
            var result = languageBLL.IsValidLanguage(language);
            Assert.That(result, Is.EqualTo(isValid));
        }

        [TestCase(-1)]
        [TestCase(10)]
        public void GetById_InvalidId_ReturnsNull(int id)
        {
            mockLanguageDAL.Setup(mr => mr.GetById(It.IsAny<int>())).Returns((int ID) => languages.Where(x => x.LanguageID == ID).FirstOrDefault());
            var result = languageBLL.GetById(id);
            Assert.IsNull(result);
        }

        [TestCase(1)]
        [TestCase(2)]
        public void GetById_ValidId_ReturnsLanguage(int id)
        {
            mockLanguageDAL.Setup(mr => mr.GetById(It.IsAny<int>())).Returns((int ID) => languages.Where(x => x.LanguageID == ID).FirstOrDefault());
            var result = languageBLL.GetById(id);
            Assert.IsNotNull(result);
            Assert.IsInstanceOf(typeof(Language), result);
        }
    }
}
