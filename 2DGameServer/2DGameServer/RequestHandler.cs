using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _2DGameServer
{
    public static class RequestHandler
    {

        public static void HandleRequest(string request)
        {
            Console.WriteLine("Executing Request: " + request);
        }
    }
}
