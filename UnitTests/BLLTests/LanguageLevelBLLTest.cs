using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using LanguageSchool.Model;
using LanguageSchool.BusinessLogic;
using LanguageSchool.DataAccess;
using Moq;

namespace UnitTests
{
    [TestFixture]
    class LanguageLevelBLLTest
    {
        Mock<ILanguageLevelDAL> mockLanguageLevelDAL;
        List<LanguageLevel> languageLevels;
        ILanguageLevelBLL languageLevelBLL;

        public LanguageLevelBLLTest()
        {
            languageLevels = new List<LanguageLevel>()
            {
                new LanguageLevel()
                {
                    LanguageLevelID=1,
                    LanguageLevelSignature="A1"
                },
                new LanguageLevel()
                {
                    LanguageLevelID=2,
                    LanguageLevelSignature="A2"
                },
                new LanguageLevel()
                {
                    LanguageLevelID=3,
                    LanguageLevelSignature="B1"
                },
                new LanguageLevel()
                {
                    LanguageLevelID=4,
                    LanguageLevelSignature="B2"
                },
                new LanguageLevel()
                {
                    LanguageLevelID=5,
                    LanguageLevelSignature="C1"
                },
                new LanguageLevel()
                {
                    LanguageLevelID=6,
                    LanguageLevelSignature="C2"
                }
            };

            mockLanguageLevelDAL = new Mock<ILanguageLevelDAL>();
            languageLevelBLL = new LanguageLevelBLL(mockLanguageLevelDAL.Object);
        }

        [Test]
        public void GetAll_Always_ReturnsAllLanguageLevels()
        {
            mockLanguageLevelDAL.Setup(mr => mr.GetAll()).Returns(languageLevels.AsQueryable());
            var result = languageLevelBLL.GetAll().Count;
            Assert.That(result, Is.EqualTo(6));
        }
    }
}
