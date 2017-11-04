using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LanguageSchool.Model;
using LanguageSchool.DataAccess;
using System.Data.Entity;

namespace LanguageSchool.BusinessLogic
{
    public class LanguageLevelBLL: ILanguageLevelBLL
    {
        private LanguageLevelDAL languageLevelDAL;

        public LanguageLevelBLL(LanguageSchoolContext context)
        {
            languageLevelDAL = new LanguageLevelDAL(context);
        }
        public DbSet<LanguageLevel> GetAll()
        {
            try
            {
                return languageLevelDAL.GetAll();
            }
            catch
            {
                throw;
            }
        }

        public List<string> GetLevels(string language)
        {
            return languageLevelDAL.GetLevels(language);
        }
    }
}
