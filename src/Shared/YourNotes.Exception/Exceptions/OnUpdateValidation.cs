using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YourNotes.Exception.Exceptions
{
    public class OnUpdateValidation : YourNotesBaseException
    {
        public OnUpdateValidation(string error)
            : base(error) { }

    }
}
