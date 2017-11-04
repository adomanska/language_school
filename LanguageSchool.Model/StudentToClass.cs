using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LanguageSchool.Model
{
    public class StudentToClass
    {
        [Key]
        [Column(Order = 0)]
        public int StudentRefID { get; set; }
        [Key]
        [Column(Order =1)]
        public int ClassRefID { get; set; }

        [ForeignKey("StudentRefID")]
        public virtual Student Student { get; set; }

        [ForeignKey("ClassRefID")]
        public virtual Class Class { get; set; }
    }
}
