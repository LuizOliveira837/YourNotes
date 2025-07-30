using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YourNotes.Communication.Responses.User
{
    public class ResponseUpdateUserName
    {
        public string Username { get; set; } = string.Empty;
        public ResponseUpdateUserName(string username)
        {
            Username = username;
        }

    }
}
