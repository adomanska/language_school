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
