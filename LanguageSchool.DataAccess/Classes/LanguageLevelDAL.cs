using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LanguageSchool.Model;
using System.Data.Entity;

namespace LanguageSchool.DataAccess
{
    public class LanguageLevelDAL: ILanguageLevelDAL
    {
        private ILanguageSchoolContext db;

        public LanguageLevelDAL(ILanguageSchoolContext context)
        {
            db = context;
            //db.LanguageLevels.Load();
        }
        public IQueryable<LanguageLevel> GetAll()
        {
            try
            {
                return db.LanguageLevels;
            }
            catch
            {
                throw;
            }
        }

        public List<string> GetLevels(string language)
        {
            var lan = db.Languages.Where(l => l.LanguageName == language).FirstOrDefault();
            if(lan != null)
            {
                return db.Classes.Where(c => c.Language.LanguageID == lan.LanguageID).Select(x=>x.LanguageLevel.LanguageLevelSignature).Distinct().ToList();
            }
            return null;
        }
    }
}
