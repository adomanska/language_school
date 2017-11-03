using System;
using System.Collections.Generic;
using System.Linq;
using System.Data.Entity;
using System.Text;
using System.Threading.Tasks;
using LanguageSchool.Model;

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

        public void Update(string emailID, string firstName, string lastName, string email, string phoneNumber)
        {
            try
            {
                Student existingStudent = FindByEmail(emailID);
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
    }
}
