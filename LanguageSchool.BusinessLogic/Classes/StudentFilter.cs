using LanguageSchool.DataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LanguageSchool.BusinessLogic
{
    public class StudentFilter
    {
        public string Text { get; set; }
        public SearchBy Filter { get; set; }
        public bool IsSorted { get; set; }

        public int PageNumber { get; set; }
        public int PageSize { get; set; }
    }
}
