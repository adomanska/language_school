using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;

namespace LanguageSchool.Model
{
    public class LanguageSchoolContext: DbContext, ILanguageSchoolContext
    {
        static LanguageSchoolContext()
        {
            Database.SetInitializer<LanguageSchoolContext>(new LanguageSchoolContextInitializer());
        }

        public IDbSet<Student> Students { get; set; }
        public IDbSet<Class> Classes { get; set; }
        public IDbSet<Language> Languages { get; set; }
        public IDbSet<LanguageLevel> LanguageLevels { get; set; }

    }
}
