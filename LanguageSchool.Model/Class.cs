using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LanguageSchool.Model
{
    public class Class
    {
        [Key]
        public int ClassID { get; set; }
        [Required]
        public string ClassName { get; set; }
        [Required]
        public string StartTime { get; set; }
        [Required]
        public string EndTime { get; set; }
        [Required]
        public DayOfWeek Day { get; set; }

        [Required]
        public int LanguageRefID { get; set; }
        [ForeignKey("LanguageRefID")]
        public virtual Language Language { get; set; }

        [Required]
        public int LanguageLevelRefID { get; set; }
        [ForeignKey("LanguageLevelRefID")]
        public virtual LanguageLevel LanguageLevel { get; set; }

        public virtual ICollection<StudentToClass> StudentToClasses { get; set; }
    }
}
