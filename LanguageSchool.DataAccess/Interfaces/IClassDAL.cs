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
        DbSet<Class> GetAll();
        void Add(Class _class);
        List<Class> GetClasess(string language, string level);
    }
}
