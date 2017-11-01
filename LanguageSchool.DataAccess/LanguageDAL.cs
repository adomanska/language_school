using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LanguageSchool.Model;
using System.Data.Entity;

namespace LanguageSchool.DataAccess
{
    public class LanguageDAL
    {
        private LanguageSchoolContext db;

        public LanguageDAL(LanguageSchoolContext context)
        {
            db = context;
            db.Languages.Load();
        }
        public DbSet<Language> GetAll()
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
    }
}
