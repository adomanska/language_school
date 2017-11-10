using LanguageSchool.BusinessLogic;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LanguageSchool.DataAccess;

namespace UnitTests
{
    [TestFixture]
    public class ClassBLLTests
    {
        private LanguageSchoolMockContext context;
        private ClassBLL classBLL;

        public ClassBLLTests()
        {
            context = new LanguageSchoolMockContext();
            classBLL = new ClassBLL(new ClassDAL(context));
        }

    }
}
