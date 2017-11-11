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
        List<Class> GetAll();
        Class GetByID(int ID);
        bool Add(string className, int startHour, int startMinute, int endHour, int endMinute, DayOfWeek day, int languageID, int languageLevelID);
        Predicate<object> GetFilterPredicate(string className, Language language, LanguageLevel languageLevel);
        List<Class> GetClasses(string language, string level);
        bool Update(int classID, string className, int startHour, int startMinute, int endHour, int endMinute, int languageID, LanguageLevel languageLevel, DayOfWeek day);
        (List<Class> classes, int pageCount) Search(ClassFilter filter);
    }
}
