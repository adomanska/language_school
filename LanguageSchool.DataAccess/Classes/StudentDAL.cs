using System;
using System.Collections.Generic;
using System.Linq;
using System.Data.Entity;
using System.Text;
using System.Threading.Tasks;
using LanguageSchool.Model;
using System.Linq.Expressions;

namespace LanguageSchool.DataAccess
{
    public enum SearchBy { Email, LastName };
    public class StudentDAL: IStudentDAL
    {
        private ILanguageSchoolContext db;

        public StudentDAL(ILanguageSchoolContext context)
        {
            db = context;
        }
        public IQueryable<Student> GetAll()
        {
            try
            {
                return db.Students;
            }
            catch
            {
                throw;
            }
        }
        public void Add(Student student)
        {
            try
            {
                db.Students.Add(student);
                db.SaveChanges();
            }
            catch
            {
                throw new Exception("Student with such email address already exists in the database");
            }
        }

        public Student FindByEmail(string email)
        {
            Student student = db.Students.FirstOrDefault(x => x.Email == email);
            return student;
        }

        public Student FindByID(int id)
        {
            Student student = db.Students.FirstOrDefault(x => x.ID == id);
            return student;
        }

        public void Update(int id, string firstName, string lastName, string email, string phoneNumber)
        {
            try
            {
                Student existingStudent = db.Students.Where(x => x.ID == id).FirstOrDefault();
                
                existingStudent.FirstName = firstName;
                existingStudent.LastName = lastName;
                existingStudent.Email = email;
                existingStudent.PhoneNumber = phoneNumber;
                
                if(db.Entry(existingStudent) != null)
                    db.Entry(existingStudent).State = System.Data.Entity.EntityState.Modified;
                db.SaveChanges();
            }
            catch
            {
                throw new Exception("Update failed");
            }
            
        }

        public IQueryable<Student> Search(SearchBy type, string text, bool sorted)
        {
            var query = db.Students.AsQueryable();
            Expression<Func<Student, string>> expression;
            if (type == SearchBy.Email)
            {
                expression = x => x.Email;
                if(text != null) query = query.Where(x => x.Email.Contains(text));
            }
            else
            {
                expression = x => x.LastName;
                if (text != null) query = query.Where(x => x.LastName.Contains(text));
            }
            query = query.OrderBy(x => x.ID);
            if (sorted)
                query = query.OrderBy(expression);
            return query;
        }

        public void SignForClass(Student student, Class languageClass)
        {
            student.Classes.Add(languageClass);
            db.SaveChanges();
        }
    }
    
}
