using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Script.Serialization;
using System.Web.Script;

namespace _2DGameServer
{
    public class RequestHandler
    {
        World world;

        public RequestHandler(ref World world)
        {
            this.world = world;
        }

        public void HandleRequest(string packets)
        {
            UserRequest userRequest = null;
            try
            {
                userRequest = new JavaScriptSerializer().Deserialize<UserRequest>(packets);
            }
            catch (Exception e)
            {
                //recieved an invalid request
                Console.WriteLine("Invalid request. Construction of userRequest failed: " + e);
            }
            
            if (world.ValidateUser(userRequest.credentials))
            {
                Console.WriteLine("Logged in");
            } else
            {
                Console.WriteLine("Not Logged in");
            }

            foreach(Request request in userRequest.requests)
            {
                Console.WriteLine(request.ID);
                //world.ApplyRequest(request);
            }
        }

    }
}
