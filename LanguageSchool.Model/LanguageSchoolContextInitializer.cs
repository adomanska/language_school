using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LanguageSchool.Model
{
    class LanguageSchoolContextInitializer : CreateDatabaseIfNotExists<LanguageSchoolContext>
    {
        protected override void Seed(LanguageSchoolContext context)
        {
            context.LanguageLevels.Add(new LanguageLevel { LanguageLevelSignature = "A1" });
            context.LanguageLevels.Add(new LanguageLevel { LanguageLevelSignature = "A2" });
            context.LanguageLevels.Add(new LanguageLevel { LanguageLevelSignature = "B1" });
            context.LanguageLevels.Add(new LanguageLevel { LanguageLevelSignature = "B2" });
            context.LanguageLevels.Add(new LanguageLevel { LanguageLevelSignature = "C1" });
            context.LanguageLevels.Add(new LanguageLevel { LanguageLevelSignature = "C2" });
        }
    }
}
