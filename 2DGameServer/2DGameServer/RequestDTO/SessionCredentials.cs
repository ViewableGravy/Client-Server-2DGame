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
        private Guid sessionToken;

        /// <summary>
        /// constructor for default construction of class
        /// </summary>
        public SessionCredentials() { }

        /// <summary>
        /// for manual creation of class (Should not be used other than in testing"
        /// </summary>
        /// <param name="sessionToken"></param>
        /// <param name="username"></param>
        public SessionCredentials(string sessionToken, string username, string TestConfirmation)
        {
            if (TestConfirmation == "Test")
            {
                this.sessionToken = Guid.Parse(sessionToken);
                this.username = username;
            }
            else
                throw new Exception();
        }

        public String SessionToken
        {
            get => sessionToken.ToString();
            set => sessionToken = Guid.Parse(value);
        }
    }
}
