using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LanguageSchool.Model;

namespace LanguageSchool.DataAccess
{
    public interface IStudentDAL
    {
        IQueryable<Student> GetAll();
        void Add(Student student);
        void SignForClass(StudentToClass studentToClass);
        Student FindByEmail(string email);
        IQueryable<Student> FindByLastName(string lastName);
        void Update(int ID, string firstName, string lastName, string email, string phoneNumber);
        IQueryable<Student> Search(SearchBy type, string text, bool sorted);

    }
}
