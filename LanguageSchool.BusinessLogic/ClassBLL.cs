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
    public class ClassBLL: IClassBLL
    {
        IClassDAL classDAL;

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
                if (className!=null && !c.ClassName.Contains(className))
                    return false;
                if (language!=null && c.LanguageRefID!= language.LanguageID)
                    return false;
                if (languageLevel!=null && c.LanguageLevelRefID != languageLevel.LanguageLevelID)
                    return false;
                else
                    return true;
            };

            return filtre;
        }

        private bool IsValidTime(string _startHour, string _startMinute, string _endHour, string _endMinute)
        {
            int startHour, startMinute, endHour=-1, endMinute;
            if(Int32.TryParse(_startHour, out startHour) && Int32.TryParse(_endHour, out endHour))
            {
                if (startHour < 0 || startHour >= 24 || endHour < 0 || endHour >= 24)
                    return false;
                if (startHour > endHour)
                    return false;
            }
            if (Int32.TryParse(_startMinute, out startMinute) && Int32.TryParse(_endMinute, out endMinute))
            {
                if (startMinute < 0 || startMinute >= 24 || endMinute < 0 || endMinute >= 24)
                    return false;
                if (startHour == endHour && startMinute > endMinute)
                    return false;
            }
            return true;
        }

        public List<Class> GetClasses(string language, string level)
        {
            return classDAL.GetClasess(language, level);
        }
    }

}
