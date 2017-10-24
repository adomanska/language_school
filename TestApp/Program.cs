using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LanguageSchoolDatabase;

namespace TestApp
{
    class Program
    {
        static void Main(string[] args)
        {
            using (var db = new LanguageSchoolContext())
            {
                var levels = from lv in db.LanguageLevels
                             select lv.LanguageLevelSignature;
                Console.WriteLine("Language Levels:");
                foreach (var el in levels)
                    Console.WriteLine(el);
            }
        }
    }
}
