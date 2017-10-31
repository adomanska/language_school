using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LanguageSchool.Model;
using System.Data.Entity;

namespace LanguageSchool.DataAccess
{
    public class LanguageLevelDAL
    {
        private LanguageSchoolContext db;

        public LanguageLevelDAL(LanguageSchoolContext context)
        {
            db = context;
            db.LanguageLevels.Load();
        }
        public DbSet<LanguageLevel> GetAll()
        {
            try
            {
                return db.LanguageLevels;
            }
            catch
            {
                throw;
            }
        }
    }
}
