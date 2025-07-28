using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YourNotes.Domain.Security
{
    public interface IJwtTokenValidator
    {

        public Guid ValidateTokenAndGetUserIdentifier(string token);
    }
}
