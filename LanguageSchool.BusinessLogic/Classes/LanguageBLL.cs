using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LanguageSchool.Model;
using LanguageSchool.DataAccess;
using Newtonsoft.Json.Linq;
using System.IO;
using System.Data.Entity;
using System.Collections.ObjectModel;
using static System.Net.Mime.MediaTypeNames;

namespace LanguageSchool.BusinessLogic
{
    public class LanguageBLL: ILanguageBLL
    {
        ILanguageDAL languageDAL;
        JArray existingLanguages;

        public LanguageBLL(ILanguageDAL _languageDAL)
        {
            languageDAL = _languageDAL;
            var directory = Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location);
            var path = string.Format("{0}\\{1}", directory, "LanguagesList.json");
            existingLanguages = JArray.Parse(@File.ReadAllText(path));
        }
        public List<Language> GetAll()
        {
            try
            {
                return languageDAL.GetAll().OrderBy(x => x.LanguageName).ToList();
            }
            catch
            {
                throw;
            }
        }

        public bool Exists(string languageName)
        {
            return GetAll().ToList().Exists(l => l.LanguageName == languageName);
        }

        public bool IsValidLanguage(string languageName)
        {
            return existingLanguages.Values().Contains(languageName);
        }
        public int Add(string languageName)
        {
            if (!existingLanguages.Values().Contains(languageName))
                throw new Exception("Language doesn't exist");
            if (Exists(languageName))
                throw new Exception("Language already exists in database");
            Language language = new Language { LanguageName = languageName };
            try
            {
                languageDAL.Add(language);
            }
            catch
            {
                throw;
            }

            return language.LanguageID;
        }

        public Language GetById(int Id)
        {
            return languageDAL.GetById(Id);
        }
    }
}
