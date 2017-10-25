using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LanguageSchool.Model
{
    public class LanguageLevel
    {
        [Key]
        public int LanguageLevelID { get; set; }
        [Required]
        public string LanguageLevelSignature { get; set; }

        public virtual ICollection<Class> Classes { get; set; }
    }
}
