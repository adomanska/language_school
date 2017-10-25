using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LanguageSchool.Model;
using LanguageSchool.DataAccess;

namespace LanguageSchool.BusinessLogic
{
    public class LanguageLevelBLL
    {
        private LanguageLevelDAL languageLevelDAL;

        public LanguageLevelBLL(LanguageSchoolContext context)
        {
            languageLevelDAL = new LanguageLevelDAL(context);
        }
        public List<LanguageLevel> GetAll()
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
    }
}
