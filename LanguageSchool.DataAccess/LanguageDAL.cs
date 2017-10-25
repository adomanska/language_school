using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LanguageSchool.Model;

namespace LanguageSchool.DataAccess
{
    public class LanguageDAL
    {
        private LanguageSchoolContext db;

        public LanguageDAL(LanguageSchoolContext context)
        {
            db = context;
        }
        public List<Language> GetAll()
        {
            try
            {
                return db.Languages.ToList();
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
