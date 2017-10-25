using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LanguageSchool.Model;
using LanguageSchool.BusinessLogic;

namespace TestApp
{
    class Program
    {
        static private StudentBLL studentBLL = new StudentBLL();
        static void Main(string[] args)
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
            Console.WriteLine();
            Console.WriteLine("---STUDENTS---");
            foreach (var st in studentBLL.GetAll())
                Console.WriteLine(st.FirstName + " " + st.LastName);
        }
    }
}
