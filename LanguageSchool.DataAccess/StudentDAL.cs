using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LanguageSchool.Model;

namespace LanguageSchool.DataAccess
{
    public class StudentDAL
    {
        private LanguageSchoolContext db;

        public StudentDAL(LanguageSchoolContext context)
        {
            db = context;
        }
        public List<Student> GetAll()
        {
            try
            {
                return db.Students.ToList();
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

        public Student FindByEmail(string email)
        {
            try
            {
                Student student = db.Students.Find(email);
                if(student == null)
                    throw new Exception("Student with such email doesn't exist");

                return student;
            }
            catch
            {
                throw;
            }
        }

        public List<Student> FindByLastName(string lastName)
        {
            try
            {
                List<Student> students = db.Students
                    .Where(st => st.LastName == lastName)
                    .ToList();

                if (students.Count == 0)
                    throw new Exception("Student with such last name doesn't exist");

                return students;
            }
            catch
            {
                throw;
            }
        }

        public void UpdateFirstName(string id, string newFirstName)
        {
            try
            {
                Student existingStudent = FindByEmail(id);
                if (existingStudent != null)
                    existingStudent.FirstName = newFirstName;

                db.Entry(existingStudent).State = System.Data.Entity.EntityState.Modified;
                db.SaveChanges();
            }
            catch(Exception ex)
            {
                throw new Exception("Update failed");
            }
            
        }
    }
}
