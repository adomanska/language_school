using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LanguageSchool.Model
{
    public class LanguageSchoolContext: DbContext
    {
        static LanguageSchoolContext()
        {
            Database.SetInitializer<LanguageSchoolContext>(new LanguageSchoolContextInitializer());
        }

        public DbSet<Student> Students { get; set; }
        public DbSet<Class> Classes { get; set; }
        public DbSet<StudentToClass> StudentsToClasses { get; set; }
        public DbSet<Language> Languages { get; set; }
        public DbSet<LanguageLevel> LanguageLevels { get; set; }
    }
}
