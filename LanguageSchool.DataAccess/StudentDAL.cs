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
    public class StudentDAL: IStudentDAL
    {
        private LanguageSchoolContext db;

        public StudentDAL(LanguageSchoolContext context)
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
            catch(Exception ex)
            {
                throw new Exception("Student with such email address already exists in the database");
            }
        }

        public void SignForClass(StudentToClass studentToClass)
        {
            try
            {
                db.StudentsToClasses.Add(studentToClass);
                db.SaveChanges();
            }
            catch(Exception ex)
            {
                throw new Exception("Student is already signed for this class");
            }
        }

        public Student FindByEmail(string email)
        {
            try
            {
                Student student = db.Students.FirstOrDefault(x => x.Email == email);
                if(student == null)
                    throw new Exception("Student with such email doesn't exist");

                return student;
            }
            catch
            {
                throw;
            }
        }

        public IQueryable<Student> FindByLastName(string lastName)
        {
            try
            {
                var students = db.Students
                    .Where(st => st.LastName == lastName);
                    
                if (students.Count() == 0)
                    throw new Exception("Student with such last name doesn't exist");

                return students;
            }
            catch
            {
                throw;
            }
        }

        public void Update(int id, string firstName, string lastName, string email, string phoneNumber)
        {
            try
            {
                Student existingStudent = db.Students.Find(id);
                if (existingStudent != null)
                {
                    existingStudent.FirstName = firstName;
                    existingStudent.LastName = lastName;
                    existingStudent.Email = email;
                    existingStudent.PhoneNumber = phoneNumber;
                }

                db.Entry(existingStudent).State = System.Data.Entity.EntityState.Modified;
                db.SaveChanges();
            }
            catch(Exception ex)
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
       
    }

    public enum SearchBy { Email, LastName};
}
