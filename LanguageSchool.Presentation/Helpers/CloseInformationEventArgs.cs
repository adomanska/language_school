using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LanguageSchool.Presentation
{
    public class CloseInformationEventArgs : EventArgs
    {
        public StudentModel ResultStudent { get; }

        public CloseInformationEventArgs(StudentModel studentModel)
        {
            ResultStudent = studentModel;
        }
    }

}
