using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LanguageSchool.BusinessLogic;
using LanguageSchool.Model;

namespace TestApp
{
    class Program
    {
        static private LanguageSchoolContext context = new LanguageSchoolContext();
        static private StudentBLL studentBLL = new StudentBLL(context);
        static private ClassBLL classBLL = new ClassBLL(context);
        static private LanguageBLL languageBLL = new LanguageBLL(context);
        static private LanguageLevelBLL languageLevelBLL = new LanguageLevelBLL(context);
        static void Main(string[] args)
        {
            Console.WriteLine("Do you want to find student? Y/N");
            string ans = Console.ReadLine();
            if (ans == "Y")
            {
                Console.Write("Email:");
                string email = Console.ReadLine();
                try
                {
                    //Student st = studentBLL.FindByEmail(email);
                    //Console.WriteLine(st.FirstName + " " + st.LastName + " " + st.Email);
                    //Console.Write("LastName:");
                    //string lastName = Console.ReadLine();
                    ////st = studentBLL.FindByLastName(lastName);
                    //Console.WriteLine(st.FirstName + " " + st.LastName + " " + st.Email);

                    ////studentBLL.Update(st.Email, "Zofia");

                }
                catch (Exception e)
                {
                    Console.WriteLine("Exception:" + e.Message);
                }

            }
            Console.WriteLine();

            Console.WriteLine("Do you want to add new student? Y/N");
            ans = Console.ReadLine();
            if (ans == "Y")
            {
                Console.WriteLine("---NEW STUDENT---");
                Console.Write("First Name:");
                string firstName = Console.ReadLine();
                Console.Write("Last Name:");
                string lastName = Console.ReadLine();
                Console.Write("Email:");
                string email = Console.ReadLine();
                try
                {
                    studentBLL.Add(firstName, lastName, email);
                }
                catch (Exception e)
                {
                    Console.WriteLine("Exception:" + e.Message);
                }
            }
            Console.WriteLine();
            Console.WriteLine("---STUDENTS---");
            foreach (var st in studentBLL.GetAll())
                Console.WriteLine(st.FirstName + " " + st.LastName);
            Console.WriteLine();

            Console.WriteLine("Do you want to add new Language? Y/N");
            ans = Console.ReadLine();
            if (ans == "Y")
            {
                Console.WriteLine("---NEW LANGUAGE---");
                Console.Write("Language Name:");
                string languageName = Console.ReadLine();
                try
                {
                    languageBLL.Add(languageName);
                }
                catch (Exception e)
                {
                    Console.WriteLine("Exception:" + e.Message);
                }
            }
            Console.WriteLine();
            Console.WriteLine("---LANGUAGES---");
            foreach (var l in languageBLL.GetAll())
                Console.WriteLine(l.LanguageID + ":" + l.LanguageName);
            Console.WriteLine();

            Console.WriteLine("---LANGUAGE LEVELS---");
            foreach (var l in languageLevelBLL.GetAll())
                Console.WriteLine(l.LanguageLevelID + ":" + l.LanguageLevelSignature);
            Console.WriteLine();

            Console.WriteLine("Do you want to add new Class? Y/N");
            ans = Console.ReadLine();
            if (ans == "Y")
            {
                Console.WriteLine("---NEW CLASS---");
                Console.Write("Class Name:");
                string className = Console.ReadLine();
                Console.Write("Language ID:");
                int languageID = Int32.Parse(Console.ReadLine());
                Console.Write("Language Level ID:");
                int languageLevelID = Int32.Parse(Console.ReadLine());
                try
                {
                    classBLL.Add(className, DateTime.Now, DateTime.Now.AddHours(2), DayOfWeek.Thursday, languageID, languageLevelID);
                }
                catch (Exception e)
                {
                    Console.WriteLine("Exception:" + e.Message);
                }
            }
            Console.WriteLine();
            Console.WriteLine("---CLASSES---");
            foreach (var c in classBLL.GetAll())
                Console.WriteLine(c.ClassName + " " + c.Language.LanguageName + " " + c.LanguageLevel.LanguageLevelSignature);
        }
    }
}
