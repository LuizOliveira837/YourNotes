using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YourNotes.Communication.Responses
{
    public class ResponseGetUser
    {
        public ResponseGetUser(string userName, string firstName, string lastName, string email)
        {
            UserName = userName;
            FirstName = firstName;
            LastName = lastName;
            Email = email;
        }

        public string UserName { get; set; } = string.Empty;
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;

    }
}
