using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LanguageSchool.Model;
using System.Data.Entity;
using System.Collections.ObjectModel;

namespace LanguageSchool.DataAccess
{
    public class LanguageDAL: ILanguageDAL
    {
        private ILanguageSchoolContext db;

        public LanguageDAL(ILanguageSchoolContext context)
        {
            db = context;
            //db.Languages.Load();
        }
        public IQueryable<Language> GetAll()
        {
            try
            {
                return db.Languages;
            }
            catch
            {
                throw;
            }
        }
        public void Add(Language language)
        {
            try
            {
                db.Languages.Add(language);
                db.SaveChanges();
            }
            catch
            {
                throw;
            }
        }

        public Language GetById(int Id)
        {
            return db.Languages.Where(x => x.LanguageID == Id).Select(x => x).FirstOrDefault();
        }
    }
}
