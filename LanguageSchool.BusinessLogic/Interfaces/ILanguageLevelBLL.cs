using LanguageSchool.Model;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LanguageSchool.BusinessLogic
{
    interface ILanguageLevelBLL
    {
        DbSet<LanguageLevel> GetAll();
        List<string> GetLevels(string language);
    }
}
