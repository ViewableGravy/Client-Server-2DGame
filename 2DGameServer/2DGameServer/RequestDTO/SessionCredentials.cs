using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _2DGameServer
{
    public class SessionCredentials
    {
        public String username;
        public Guid sessionToken;

        public SessionCredentials(String username, String sessionToken)
        {
            this.username = username;
            this.sessionToken = Guid.Parse(sessionToken);
        }
    }
}
