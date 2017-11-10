using System;
using System.Collections.Generic;
using System.Linq;
using System.Data.Entity;
using System.Text;
using System.Threading.Tasks;
using LanguageSchool.Model;

namespace UnitTests
{
    class LanguageSchoolMockContext : ILanguageSchoolContext
    {
        readonly mockdb


        public IDbSet<Student> Students => throw new NotImplementedException();

        public IDbSet<Class> Classes => throw new NotImplementedException();

        public IDbSet<StudentToClass> StudentsToClasses => throw new NotImplementedException();

        public IDbSet<Language> Languages => throw new NotImplementedException();

        public IDbSet<LanguageLevel> LanguageLevels => throw new NotImplementedException();

        public void Dispose()
        {
        }
    }
}
