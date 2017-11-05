using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PropertyChanged;

namespace LanguageSchool.Presentation
{
    [AddINotifyPropertyChangedInterface]
    class LanguageLevelModel
    {
        public int LanguageLevelID { get; set; }
        public string LanguageLevelSignature { get; set; }
    }
}
