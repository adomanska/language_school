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
        public string StudentRefID { get; set; }
        [Key]
        [Column(Order =1)]
        public int ClassRefID { get; set; }

        [ForeignKey("StudentRefID")]
        public Student Student { get; set; }

        [ForeignKey("ClassRefID")]
        public Class Class { get; set; }
    }
}
