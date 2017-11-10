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
        private ILanguageSchoolContext db;

        public ClassDAL(ILanguageSchoolContext context)
        {
            db = context;
            //db.Classes.Load();
        }
        public IQueryable<Class> GetAll()
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

        public Class GetByID(int ID)
        {
            Class _class = db.Classes.Where(x => x.ClassID == ID).Select(x => x).FirstOrDefault();
            return _class;
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

        public void Update(int classID, string className,string startTime, string endTime, int languageID, int languageLevelID, DayOfWeek day)
        {
            try
            {
                var obj = db.Classes.Where(x => x.ClassID == classID).FirstOrDefault();
                obj.ClassName = className;
                obj.LanguageRefID = languageID;
                obj.LanguageLevelRefID = languageLevelID;
                obj.Day = day;
                obj.StartTime = startTime;
                obj.EndTime = endTime;
                if(db.Entry(obj)!=null)
                    db.Entry(obj).State = System.Data.Entity.EntityState.Modified;
                db.SaveChanges();
            }
            catch(Exception e)
            {
                throw new Exception("Class cannot be updated");
            }
        }

        public IQueryable<Class> Search (string className, int languageID, int languageLevelID)
        {
            IQueryable<Class> resultCollection = db.Classes.AsQueryable();

            if (languageID != -1)
                resultCollection = resultCollection.Where(x => x.LanguageRefID == languageID);
            if (languageLevelID != -1)
                resultCollection = resultCollection.Where(x => x.LanguageLevelRefID == languageLevelID);
            if (className != null)
                resultCollection = resultCollection.Where(x => x.ClassName.Contains(className));

            return resultCollection;
        }
     
    }
}
