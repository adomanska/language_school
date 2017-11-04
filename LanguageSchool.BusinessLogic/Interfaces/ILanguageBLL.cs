﻿using LanguageSchool.Model;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LanguageSchool.BusinessLogic
{
    public interface ILanguageBLL
    {
        DbSet<Language> GetAll();
        int Add(string languageName);
        bool Exists(string languageName);
        bool IsValidLanguage(string languageName);
    }
}
