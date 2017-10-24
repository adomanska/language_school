using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LanguageSchoolDatabase
{
    public enum DayOfWeek { Monday, Tuesday, Wednesday, Thursday, Friday}
    public class Class
    {
        [Key]
        public int ClassID { get; set; }
        [Required]
        public string ClassName { get; set; }
        [Required]
        public DateTime StartTime { get; set; }
        [Required]
        public DateTime EndTime { get; set; }
        [Required]
        public DayOfWeek Day { get; set; }

        [Required]
        public int LanguageRefID { get; set; }
        [ForeignKey("LanguageRefID")]
        public Language Language { get; set; }

        [Required]
        public int LanguageLevelRefID { get; set; }
        [ForeignKey("LanguageLevelRefID")]
        public LanguageLevel LanguageLevel { get; set; }

        public ICollection<StudentToClass> StudentToClasses { get; set; }
    }
}
