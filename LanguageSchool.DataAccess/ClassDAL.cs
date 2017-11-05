using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LanguageSchool.Model;
using System.Data.Entity;

namespace LanguageSchool.DataAccess
{
    public class ClassDAL: IClassDAL
    {
        private LanguageSchoolContext db;

        public ClassDAL(LanguageSchoolContext context)
        {
            db = context;
            db.Classes.Load();
        }
        public DbSet<Class> GetAll()
        {
            try
            {
                return db.Classes;
            }
            catch
            {
                throw;
            }
        }
        public void Add(Class _class)
        {
            try
            {
                db.Classes.Add(_class);
                db.SaveChanges();
            }
            catch
            {
                throw;
            }
        }
        
        public List<Class> GetClasess(string language, string level)
        {
            return db.Classes.Where(x => x.LanguageLevel.LanguageLevelSignature == level && x.Language.LanguageName == language).ToList();
        }

        public void Update(Class _class, string className, Language language, LanguageLevel languageLevel, DayOfWeek day)
        {
            try
            {
                _class.ClassName = className;
                _class.Language = language;
                _class.LanguageLevel = languageLevel;
                _class.Day = day;
                db.Entry(_class).State = System.Data.Entity.EntityState.Modified;
                db.SaveChanges();
            }
            catch
            {
                throw new Exception("Class cannot be updated");
            }
        }
     
    }
}
