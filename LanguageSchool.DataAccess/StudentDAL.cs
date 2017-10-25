﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LanguageSchool.Model;

namespace LanguageSchool.DataAccess
{
    public class StudentDAL
    {
        private LanguageSchoolContext db = new LanguageSchoolContext();
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
            catch(Exception e)
            {
                throw new Exception("Student with such email address already exists in the database");
            }
        }
    }
}