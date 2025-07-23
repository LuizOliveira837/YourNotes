using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YourNotes.Communication.Responses
{
    public class ResponseRegisterUser
    {
        public ResponseRegisterUser(Guid id)
        {
            Id = id;
        }

        public Guid Id { get; set; }
    }
}
