using LanguageSchool.Model;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LanguageSchool.BusinessLogic
{
    public interface IClassBLL
    {
        DbSet<Class> GetAll();
        void Add(string className, DateTime startTime, DateTime endTime, DayOfWeek day, int languageID, int languageLevelID);
        Predicate<object> GetFilterPredicate(string className, Language language, LanguageLevel languageLevel);
        List<Class> GetClasses(string language, string level);
        void Update(Class _class, string className, Language language, LanguageLevel languageLevel, DayOfWeek day);
    }
}
