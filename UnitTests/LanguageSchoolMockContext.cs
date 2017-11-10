using System;
using System.Collections.Generic;
using System.Linq;
using System.Data.Entity;
using System.Text;
using System.Threading.Tasks;
using LanguageSchool.Model;
using System.Data.Entity.Infrastructure;

namespace UnitTests
{
    class LanguageSchoolMockContext : ILanguageSchoolContext
    {
        readonly MockDbSet<Student> _students;
        readonly MockDbSet<Class> _clasess;
        readonly MockDbSet<Language> _languages;
        readonly MockDbSet<LanguageLevel> _languageLevels;
        readonly MockDbSet<StudentToClass> _studentToClass;

        public LanguageSchoolMockContext() 
        {
            List<Student> students = new List<Student>()
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
                },
                new Student()
                {
                   ID = 4,
                   FirstName = "Kate",
                   LastName = "King",
                   Email = "king@gmail.com",
                   PhoneNumber = null,
                },
                new Student()
                {
                   ID = 5,
                   FirstName = "John",
                   LastName = "Davis",
                   Email = "davisj@gmail.com",
                   PhoneNumber = "456321789"
                }
            };

            _students = new MockDbSet<Student>(students);

            List<Language> languages = new List<Language>()
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
                    LanguageID=4,
                    LanguageName="Russian"
                }
            };
            _languages = new MockDbSet<Language>(languages);

            List<Class> classes = new List<Class>()
            {
                new Class()
                {
                    ClassID=1,
                    ClassName="English M1",
                    LanguageRefID=1,
                    LanguageLevelRefID=1,
                    StartTime="10:00",
                    EndTime="11:30",
                    Day=DayOfWeek.Monday
                },
                new Class()
                {
                    ClassID=2,
                    ClassName="English M14",
                    LanguageRefID=1,
                    LanguageLevelRefID=5,
                    StartTime="10:00",
                    EndTime="11:30",
                    Day=DayOfWeek.Tuesday
                },
                new Class()
                {
                    ClassID=3,
                    ClassName="Spanish M2",
                    LanguageRefID=2,
                    LanguageLevelRefID=1,
                    StartTime="11:00",
                    EndTime="12:30",
                    Day=DayOfWeek.Monday
                },
                new Class()
                {
                    ClassID=4,
                    ClassName="Spanish Conversations",
                    LanguageRefID=2,
                    LanguageLevelRefID=4,
                    StartTime="10:00",
                    EndTime="11:30",
                    Day=DayOfWeek.Thursday
                },
                new Class()
                {
                    ClassID=5,
                    ClassName="Russian M15",
                    LanguageRefID=3,
                    LanguageLevelRefID=5,
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
                    StartTime="10:00",
                    EndTime="11:30",
                    Day=DayOfWeek.Friday
                },
            };
            _clasess = new MockDbSet<Class>(classes);
        }
        public IDbSet<Student> Students => _students.Set.Object;

        public IDbSet<Class> Classes => _clasess.Set.Object;

        public IDbSet<StudentToClass> StudentsToClasses => _studentToClass.Set.Object;

        public IDbSet<Language> Languages => _languages.Set.Object;

        public IDbSet<LanguageLevel> LanguageLevels => _languageLevels.Set.Object;

        public void Dispose()
        {
        }

        public int SaveChanges()
        {
            return 0;
        }

        public DbEntityEntry Entry(Object o)
        {
            return null;
        }
    }
}
