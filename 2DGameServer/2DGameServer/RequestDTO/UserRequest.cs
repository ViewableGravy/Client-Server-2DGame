using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _2DGameServer
{
    public class UserRequest
    {
        public SessionCredentials credentials;
        public List<Request> requests;
    }
}
