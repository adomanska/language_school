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
        void SignForClass(Student student, Class _class);
        void Update(int ID, string firstName, string lastName, string email, string phoneNumber);
        Student FindByEmail(string email);
        Student FindByID(int id);
        IQueryable<Student> Search(SearchBy type, string text, bool sorted);

    }
}
