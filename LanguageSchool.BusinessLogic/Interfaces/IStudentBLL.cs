using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LanguageSchool.DataAccess;
using LanguageSchool.Model;

namespace LanguageSchool.BusinessLogic
{
    public interface IStudentBLL
    {
        List<Student> GetAll();
        void Add(string firstName, string lastName, string email, string phoneNumber = "");
        void SignForClass(int studentID, Class languageClass);
        Student FindByEmail(string email);
        Predicate<object> GetFilterByEmailPredicate(string email);
        Predicate<object> GetFilterByLastNamePredicate(string lastName);
        IQueryable<Student> FindByLastName(string lastName);
        void Update(int id, string firstName, string lastName, string email, string phoneNumber = "");
        (List<Student> students, int pageCount) Search(StudentFilter filter);

    }
}
