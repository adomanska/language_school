using LanguageSchool.Model;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LanguageSchool.DataAccess
{
    public interface IClassDAL
    {
        IQueryable<Class> GetAll();
        void Add(Class _class);
        List<Class> GetClasess(string language, string level);
        void Update(int classID, string className,string startTime, string endTime, int languageID, int languageLevelID, DayOfWeek day);
        IQueryable<Class> Search(string className, int languageID, int languageLevelID);
    }
}
