using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LanguageSchool.Model;
using System.Data.Entity;

namespace LanguageSchool.DataAccess
{
    public class ClassDAL
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
     
    }
}
