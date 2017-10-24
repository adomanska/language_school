﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LanguageSchoolDatabase
{
    public class Language
    {
        [Key]
        public int LanguageID { get; set; }
        [Required]
        public string LanguageName { get; set; }

        public ICollection<Class> Classes { get; set; }
    }
}
