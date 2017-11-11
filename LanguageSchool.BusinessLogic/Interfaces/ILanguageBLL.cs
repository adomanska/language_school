using LanguageSchool.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LanguageSchool.BusinessLogic
{
    public interface ILanguageBLL
    {
        List<Language> GetAll();
        int Add(string languageName);
        bool Exists(string languageName);
        bool IsValidLanguage(string languageName);
        Language GetById(int Id);
    }
}
