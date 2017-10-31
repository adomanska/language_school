using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LanguageSchool.DataAccess;
using LanguageSchool.Model;
using System.Data.Entity;
using System.Collections.ObjectModel;

namespace LanguageSchool.BusinessLogic
{
    public class ClassBLL
    {
        private ClassDAL classDAL;

        public ClassBLL(LanguageSchoolContext context)
        {
            classDAL = new ClassDAL(context);
        }
        public DbSet<Class> GetAll()
        {
            try
            {
                return classDAL.GetAll();
            }
            catch
            {
                throw;
            }
        }
        public void Add(string className, DateTime startTime, DateTime endTime, DayOfWeek day, int languageID, int languageLevelID )
        {
            if (endTime.CompareTo(startTime) < 0)
                throw new Exception("Start Time cannot be earlier than End Time");
            Class _class = new Class { ClassName = className, StartTime = startTime, EndTime = endTime, Day = day, LanguageRefID=languageID, LanguageLevelRefID=languageLevelID };
            try
            {
                classDAL.Add(_class);
            }
            catch
            {
                throw;
            }
        }

        public Predicate<object> GetFilterPredicate(string className, Language language, LanguageLevel languageLevel)
        {
            Predicate<object> filtre = o =>
            {
                Class c = o as Class;
                if (!c.ClassName.Contains(className))
                    return false;
                if (language!=null && c.Language != language)
                    return false;
                if (languageLevel!=null && c.LanguageLevel != languageLevel)
                    return false;
                else
                    return true;
            };

            return filtre;
        }
    }
}
