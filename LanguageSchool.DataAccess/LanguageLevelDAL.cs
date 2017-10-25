using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LanguageSchool.Model;

namespace LanguageSchool.DataAccess
{
    public class LanguageLevelDAL
    {
        private LanguageSchoolContext db;

        public LanguageLevelDAL(LanguageSchoolContext context)
        {
            db = context;
        }
        public List<LanguageLevel> GetAll()
        {
            try
            {
                return db.LanguageLevels.ToList();
            }
            catch
            {
                throw;
            }
        }
    }
}
