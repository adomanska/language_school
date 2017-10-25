using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LanguageSchool.Model;
using LanguageSchool.DataAccess;

namespace LanguageSchool.BusinessLogic
{
    public class LanguageBLL
    {
        private LanguageDAL languageDAL;

        public LanguageBLL(LanguageSchoolContext context)
        {
            languageDAL = new LanguageDAL(context);
        }
        public List<Language> GetAll()
        {
            try
            {
                return languageDAL.GetAll();
            }
            catch
            {
                throw;
            }
        }
        public void Add(string languageName)
        {
            Language language = new Language { LanguageName = languageName };
            try
            {
                languageDAL.Add(language);
            }
            catch
            {
                throw;
            }
        }
    }
}
