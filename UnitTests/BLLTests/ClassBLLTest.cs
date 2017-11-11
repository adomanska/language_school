using LanguageSchool.BusinessLogic;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LanguageSchool.DataAccess;
using LanguageSchool.Model;

namespace UnitTests
{
    [TestFixture]
    public class ClassBLLTests
    {
        private Mock<IClassDAL> mockClassDAL;
        private IQueryable<Class> classes;
        private IClassBLL classBLL;
        private List<LanguageLevel> languageLevels;
        private List<Language> languages;

        public ClassBLLTests()
        {
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

            classes = new List<Class>()
            {
                new Class()
                {
                    ClassID=1,
                    ClassName="English M1",
                    LanguageRefID=1,
                    Language = languages.ElementAt(0),
                    LanguageLevelRefID=1,
                    LanguageLevel = languageLevels.ElementAt(0),
                    StartTime="10:00",
                    EndTime="11:30",
                    Day=DayOfWeek.Monday
                },
                new Class()
                {
                    ClassID=2,
                    ClassName="English M14",
                    LanguageRefID=1,
                    Language = languages.ElementAt(0),
                    LanguageLevelRefID=5,
                    LanguageLevel = languageLevels.ElementAt(4),
                    StartTime="10:00",
                    EndTime="11:30",
                    Day=DayOfWeek.Tuesday
                },
                new Class()
                {
                    ClassID=3,
                    ClassName="Spanish M2",
                    LanguageRefID=2,
                    Language = languages.ElementAt(1),
                    LanguageLevelRefID=1,
                    LanguageLevel = languageLevels.ElementAt(0),
                    StartTime="11:00",
                    EndTime="12:30",
                    Day=DayOfWeek.Monday
                },
                new Class()
                {
                    ClassID=4,
                    ClassName="Spanish Conversations",
                    LanguageRefID=2,
                    Language = languages.ElementAt(1),
                    LanguageLevelRefID=4,
                    LanguageLevel = languageLevels.ElementAt(3),
                    StartTime="10:00",
                    EndTime="11:30",
                    Day=DayOfWeek.Thursday
                },
                new Class()
                {
                    ClassID=5,
                    ClassName="Russian M15",
                    LanguageRefID=3,
                    Language = languages.ElementAt(2),
                    LanguageLevelRefID=5,
                    LanguageLevel = languageLevels.ElementAt(4),
                    StartTime="12:00",
                    EndTime="13:30",
                    Day=DayOfWeek.Wednesday
                },
                new Class()
                {
                    ClassID=6,
                    ClassName="Russian M1",
                    LanguageRefID=3,
                    LanguageLevelRefID=1,
                    LanguageLevel = languageLevels.ElementAt(0),
                    Language = languages.ElementAt(2),
                    StartTime="10:00",
                    EndTime="11:30",
                    Day=DayOfWeek.Friday
                },
            }.AsQueryable();

            mockClassDAL = new Mock<IClassDAL>();
            classBLL = new ClassBLL(mockClassDAL.Object);
        }

        [Test]
        public void GetAll_Always_ReturnsAllClasses()
        {
            mockClassDAL.Setup(mr => mr.GetAll()).Returns(classes);
            var result = classBLL.GetAll().Count;
            Assert.That(result, Is.EqualTo(6));
        }

        [TestCase(-1)]
        [TestCase(10)]
        public void GetById_InvalidId_ReturnsNull(int ID)
        {
            mockClassDAL.Setup(mr => mr.GetByID(It.IsAny<int>())).Returns((int id) => classes.Where(x => x.ClassID == id).FirstOrDefault());
            var result = classBLL.GetByID(ID);
            Assert.IsNull(result);
        }

        [TestCase(1)]
        [TestCase(4)]
        public void GetById_ValidId_ReturnsClass(int ID)
        {
            mockClassDAL.Setup(mr => mr.GetByID(It.IsAny<int>())).Returns((int id) => classes.Where(x => x.ClassID == id).FirstOrDefault());
            var result = classBLL.GetByID(ID);
            Assert.IsNotNull(result);
            Assert.IsInstanceOf(typeof(Class), result);
        }

        [TestCase("English", "C1", 1)]
        [TestCase("Russian", "A1", 1)]
        public void GetClasses_Always_ReturnsExpectedResult(string lang, string level, int count)
        {
            mockClassDAL.Setup(mr => mr.GetClasess(It.IsAny<string>(), It.IsAny<string>())).Returns(
               (string language, string languageLevel) => classes.Where(x => x.Language.LanguageName == language && x.LanguageLevel.LanguageLevelSignature == languageLevel).ToList());
            var result = classBLL.GetClasses(lang, level).Count;
            Assert.That(result, Is.EqualTo(count));
        }

        [TestCase("M",-1,-1,3,2,1,3)]
        [TestCase(null,1,-1,2,1,1,2)]
        public void Search_Always_ReturnsExpectedResult(string className, int languageID, int languageLevelID, int pageNumber, int pageSize, int classCount, int pagesCount)
        {
            mockClassDAL.Setup(mr => mr.Search(It.IsAny<string>(), It.IsAny<int>(), It.IsAny<int>())).Returns(
               (string name, int langID, int levelID) =>
               {
                   var result = classes;
                   if (name != null)
                       result = result.Where(x => x.ClassName.Contains(name));
                   if (langID != -1)
                       result = result.Where(x => x.LanguageRefID == langID);
                   if (levelID != -1)
                       result = result.Where(x => x.LanguageLevelRefID == levelID);
                   return result;
               });
            ClassFilter filter = new ClassFilter()
            {
                ClassName = className,
                Language = languageID!=-1 ? languages.ElementAt(languageID - 1) : null,
                LanguageLevel = languageLevelID!=-1 ? languageLevels.ElementAt(languageLevelID - 1) : null,
                PageNumber = pageNumber,
                PageSize = pageSize
            };

            (var res1, var res2) = classBLL.Search(filter);
            Assert.That(res1.Count, Is.EqualTo(classCount));
            Assert.That(res2, Is.EqualTo(pagesCount));
        }
    }
}
