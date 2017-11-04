﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LanguageSchool.Model;
using LanguageSchool.DataAccess;
using Newtonsoft.Json.Linq;
using System.IO;
using System.Data.Entity;

namespace LanguageSchool.BusinessLogic
{
    public class LanguageBLL: ILanguageBLL
    {
        private LanguageDAL languageDAL;
        JArray existingLanguages;

        public LanguageBLL(LanguageSchoolContext context)
        {
            languageDAL = new LanguageDAL(context);
            existingLanguages = JArray.Parse(@File.ReadAllText("LanguagesList.json"));
        }
        public DbSet<Language> GetAll()
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

        private bool Exists(string languageName)
        {
            return GetAll().ToList().Exists(l => l.LanguageName == languageName);
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
    }
}
