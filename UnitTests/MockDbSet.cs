using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LanguageSchool.Model;
using Moq;

namespace UnitTests
{
    public class MockDbSet<T> where T : class
    {
        /// <summary>
        /// Pobiera zmockowany IDbSet
        /// </summary>
        public Mock<IDbSet<T>> Set { get; }

        /// <summary>
        /// Tworzy nową instancję obiektu z listą danych
        /// </summary>
        /// <param name="data">Lista danych</param>
        public MockDbSet(IList<T> data)
        {
            Mock<IDbSet<T>> set = new Mock<IDbSet<T>>();
            set.As<IQueryable<T>>().Setup(m => m.Provider).Returns(data.AsQueryable().Provider);
            set.As<IQueryable<T>>().Setup(m => m.Expression).Returns(data.AsQueryable().Expression);
            set.As<IQueryable<T>>().Setup(m => m.ElementType).Returns(data.AsQueryable().ElementType);
            set.As<IQueryable<T>>().Setup(m => m.GetEnumerator()).Returns(data.AsQueryable().GetEnumerator());

            set.Setup(s => s.Add(It.IsAny<T>())).Returns((T t) => t).Callback((T t) => data.Add(t));
            set.Setup(s => s.Remove(It.IsAny<T>())).Returns((T t) => t).Callback((T t) => data.Remove(t));
            Set = set;
        }
    }
}
