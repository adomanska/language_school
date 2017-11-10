using LanguageSchool.Model;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LanguageSchool.DataAccess
{
    public interface ILanguageDAL
    {
        IDbSet<Language> GetAll();
        Language GetById(int Id);
        void Add(Language language);
    }
}
