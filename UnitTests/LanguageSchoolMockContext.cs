using System;
using System.Collections.Generic;
using System.Linq;
using System.Data.Entity;
using System.Text;
using System.Threading.Tasks;
using LanguageSchool.Model;
using System.Data.Entity.Infrastructure;
using System.Collections.ObjectModel;

namespace UnitTests
{
    class LanguageSchoolMockContext : ILanguageSchoolContext
    {
        readonly MockDbSet<Student> _students;
        readonly MockDbSet<Class> _clasess;
        readonly MockDbSet<Language> _languages;
        readonly MockDbSet<LanguageLevel> _languageLevels;

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
                   PhoneNumber = "536987415",
                   Classes = new Collection<Class>()
                },
                new Student()
                {
                   ID = 2,
                   FirstName = "Tom",
                   LastName = "Brown",
                   Email = "tomb@gmail.com",
                   PhoneNumber = "236859714",
                   Classes = new Collection<Class>()
                },
                new Student()
                {
                   ID = 3,
                   FirstName = "Elizabeth",
                   LastName = "Jones",
                   Email = "elizabeth@gmail.com",
                   PhoneNumber = "444555236",
                   Classes = new Collection<Class>()
                },
                new Student()
                {
                   ID = 4,
                   FirstName = "Kate",
                   LastName = "King",
                   Email = "king@gmail.com",
                   PhoneNumber = null,
                   Classes = new Collection<Class>()
                },
                new Student()
                {
                   ID = 5,
                   FirstName = "John",
                   LastName = "Davis",
                   Email = "davisj@gmail.com",
                   PhoneNumber = "456321789",
                   Classes = new Collection<Class>()
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
                    LanguageID=3,
                    LanguageName="Russian"
                }
            };
            _languages = new MockDbSet<Language>(languages);

            List<LanguageLevel> languageLevels = new List<LanguageLevel>()
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
            _languageLevels = new MockDbSet<LanguageLevel>( languageLevels);
            List<Class> classes = new List<Class>()
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
                    Day=DayOfWeek.Monday,
                    Students = new Collection<Student>()
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
                    Day=DayOfWeek.Tuesday,
                    Students = new Collection<Student>()
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
                    Day=DayOfWeek.Monday,
                    Students = new Collection<Student>()
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
                    Day=DayOfWeek.Thursday,
                    Students = new Collection<Student>()
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
                    Day=DayOfWeek.Wednesday,
                    Students = new Collection<Student>()
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
                    Day=DayOfWeek.Friday,
                    Students = new Collection<Student>()
                },
            };
            _clasess = new MockDbSet<Class>(classes);

        }
        public IDbSet<Student> Students => _students.Set.Object;

        public IDbSet<Class> Classes => _clasess.Set.Object;

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
