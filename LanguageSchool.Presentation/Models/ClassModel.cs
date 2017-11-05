using LanguageSchool.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PropertyChanged;

namespace LanguageSchool.Presentation
{
    [AddINotifyPropertyChangedInterface]
    public class ClassModel
    {
        public int ClassID { get; set; }
        public string ClassName { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
        public DayOfWeek Day { get; set; }
        public Language Language { get; set; }
        public LanguageLevel LanguageLevel { get; set; }
    }
}
