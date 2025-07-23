using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YourNotes.Exception.Exceptions
{
    public class YourNotesBaseException : SystemException
    {
        public string Error { get; set; } = string.Empty;


        public YourNotesBaseException(string error)
            :base(error)
        {
            Error = error;
        }

    }
}
