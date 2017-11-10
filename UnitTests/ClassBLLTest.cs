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
        //private LanguageSchoolMockContext context;
        //private ClassBLL classBLL;
        //private LanguageBLL languageBLL;
        //private LanguageLevelBLL languageLevelBLL;

        //public ClassBLLTests()
        //{
        //    context = new LanguageSchoolMockContext();
        //    classBLL = new ClassBLL(new ClassDAL(context));
        //    languageBLL = new LanguageBLL(new LanguageDAL(context));
        //    languageLevelBLL = new LanguageLevelBLL(new LanguageLevelDAL(context));
        //}

        //[Test]
        //public void GetAll_Always_ReturnAllClasses()
        //{
        //    int classesCount = classBLL.GetAll().Count;
        //    Assert.That(classesCount, Is.EqualTo(6));
        //}

        //[TestCase(null)]
        //[TestCase("")]
        //public void Add_InvalidClassName_False(string className)
        //{
        //    var result = classBLL.Add(className, 9, 30, 11, 0, DayOfWeek.Monday, 1, 1);
        //    Assert.That(result, Is.EqualTo(false));
        //}

        //[TestCase(20,45,19,0)]
        //[TestCase(26,0,0,0)]
        //public void Add_InvalidTime_False(int startHour, int startMinute, int endHour, int endMinute)
        //{
        //    var result = classBLL.Add("Sample Class Name", startHour, startMinute, endHour, endMinute, DayOfWeek.Friday, 1, 1);
        //    Assert.That(result, Is.EqualTo(false));
        //}

        ////[TestCase(10)]
        ////[TestCase(20)]
        ////public void Add_InvalidLanguage_Exception(int languageID)
        ////{
        ////    Assert.Throws<Exception>(() => classBLL.Add("Sample class name", 9, 30, 11, 0, DayOfWeek.Thursday, languageID, 1));
        ////}

        //[TestCase(10)]
        //[TestCase(20)]
        //public void Add_InvalidLanguageLevel_False(int languageLevelID)
        //{
        //    var result =  classBLL.Add("Sample class name", 9, 30, 11, 0, DayOfWeek.Thursday, 1, languageLevelID);
        //    Assert.That(result, Is.EqualTo(false));
        //}

        //[TestCase(null)]
        //[TestCase("")]
        //public void Update_InvalidClassName_False(string className)
        //{
        //    var result = classBLL.Update(1, className, 9, 30, 11, 0, 1, languageLevelBLL.GetAll().First(), DayOfWeek.Thursday);
        //    Assert.That(result, Is.EqualTo(false));
        //}

        //[TestCase(20, 45, 19, 0)]
        //[TestCase(26, 0, 0, 0)]
        //public void Update_InvalidTime_False(int startHour, int startMinute, int endHour, int endMinute)
        //{
        //    var result = classBLL.Update(1,"Sample Class Name", startHour, startMinute, endHour, endMinute, 1, languageLevelBLL.GetAll().First(), DayOfWeek.Thursday);
        //    Assert.That(result, Is.EqualTo(false));
        //}

        ////[TestCase(10)]
        ////[TestCase(20)]
        ////public void Add_InvalidLanguage_Exception(int languageID)
        ////{
        ////    Assert.Throws<Exception>(() => classBLL.Add("Sample class name", 9, 30, 11, 0, DayOfWeek.Thursday, languageID, 1));
        ////}

        //[Test]
        //public void Update_InvalidLanguageLevel_False()
        //{
        //    var result = classBLL.Update(1, "Sample Class Name", 9, 30, 11, 0, 1, null, DayOfWeek.Thursday);
        //    Assert.That(result, Is.EqualTo(false));
        //}

    }
}
