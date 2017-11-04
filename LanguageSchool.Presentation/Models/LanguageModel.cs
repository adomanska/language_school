using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PropertyChanged;

namespace LanguageSchool.Presentation
{
    [AddINotifyPropertyChangedInterface]
    public class LanguageModel
    {
        public int LanguageID { get; set; }
        public string LanguageName { get; set; }
    }
}
