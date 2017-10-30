using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LanguageSchool.Model;
using LanguageSchool.DataAccess;
using Newtonsoft.Json.Linq;
using System.IO;

namespace LanguageSchool.BusinessLogic
{
    public class LanguageBLL
    {
        private LanguageDAL languageDAL;
        JArray existingLanguages;

        public LanguageBLL(LanguageSchoolContext context)
        {
            languageDAL = new LanguageDAL(context);
            existingLanguages = JArray.Parse(@File.ReadAllText("LanguagesList.json"));
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
            if (!existingLanguages.Values().Contains(languageName))
                throw new Exception("Language doesn't exist");
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
