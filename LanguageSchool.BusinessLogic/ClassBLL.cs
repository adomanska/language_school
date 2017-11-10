﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LanguageSchool.DataAccess;
using LanguageSchool.Model;
using System.Data.Entity;
using System.Collections.ObjectModel;

namespace LanguageSchool.BusinessLogic
{
    public class ClassBLL: IClassBLL
    {
        IClassDAL classDAL;

        public ClassBLL(IClassDAL _classDAL)
        {
            classDAL = _classDAL;
        }
        public List<Class> GetAll()
        {
            try
            {
                return classDAL.GetAll().ToList();
            }
            catch
            {
                throw;
            }
        }

        public Class GetByID (int ID)
        {
            return classDAL.GetByID(ID);
        }

        public int Add(string className, int startHour, int startMinute, int endHour, int endMinute, DayOfWeek day, int languageID, int languageLevelID )
        {
            Class _class = new Class {
                ClassName = className,
                StartTime = startHour.ToString("00")+":"+startMinute.ToString("00"),
                EndTime = endHour.ToString("00")+":"+endMinute.ToString("00"),
                Day = day,
                LanguageRefID = languageID,
                LanguageLevelRefID = languageLevelID
            };
            try
            {
                classDAL.Add(_class);
            }
            catch
            {
                throw;
            }

            return _class.ClassID;
        }

        public void Update(int classID, string className, int startHour, int startMinute, int endHour, int endMinute, int languageID, LanguageLevel languageLevel, DayOfWeek day)
        {
            try
            {
                classDAL.Update(classID, className, startHour.ToString("00")+":"+startMinute.ToString("00"), endHour.ToString("00")+":"+endMinute.ToString("00"), languageID, languageLevel.LanguageLevelID, day);
            }
            catch
            {
                throw;
            }
        }
        public Predicate<object> GetFilterPredicate(string className, Language language, LanguageLevel languageLevel)
        {
            Predicate<object> filtre = o =>
            {
                Class c = o as Class;
                if (className != null && !c.ClassName.Contains(className))
                    return false;
                if (language != null && c.LanguageRefID != language.LanguageID)
                    return false;
                if (languageLevel != null && c.LanguageLevelRefID != languageLevel.LanguageLevelID)
                    return false;
                else
                    return true;
            };

            return filtre;
        }

        public bool IsValidTime(int startHour, int startMinute, int endHour, int endMinute)
        {
            if (startHour > endHour)
                return false;
            if (startHour == endHour && startMinute > endMinute)
                return false;
            return true;
        }

        public List<Class> GetClasses(string language, string level)
        {
            return classDAL.GetClasess(language, level);
        }

        public (List<Class> classes, int pageCount) Search(ClassFilter filter)
        {
            var resultCollection = classDAL.Search(filter.ClassName, filter.Language == null ? -1 : filter.Language.LanguageID, filter.LanguageLevel == null ? -1 : filter.LanguageLevel.LanguageLevelID);
            var count = Math.Ceiling(((double)resultCollection.Count()) / filter.PageSize);
            var list = resultCollection.OrderBy(x=> x.ClassName).Skip(filter.PageSize * (filter.PageNumber - 1)).Take(filter.PageSize).ToList();

            return (list, (int)count);
        }
    }

    public class ClassFilter
    {
        public string ClassName { get; set; }
        public Language Language { get; set; }
        public LanguageLevel LanguageLevel{ get; set; }

        public int PageNumber { get; set; }
        public int PageSize { get; set; }
    }
}
