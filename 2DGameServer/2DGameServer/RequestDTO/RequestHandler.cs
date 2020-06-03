using Newtonsoft.Json;
using System;

namespace _2DGameServer
{
    public class RequestHandler
    {
        World world;

        public RequestHandler(ref World world)
        {
            this.world = world;
        }

        public void HandleRequest(UserRequest request)
        {
            if (request == null)
                return;

            //validate request is from a valid user
            if (world.ValidateUser(request.credentials))
            {

                //TODO: implement A system for wrapping the ID's after they get to x value

                Player player = world.GetUser(request.credentials.username);
                var requests = request.requests;

                int indexsFromLast = requests[requests.Count - 1].ID - player.mostRecentRequest;

                if (indexsFromLast <= 0)
                    return;

                for (int i = requests.Count - indexsFromLast; i < requests.Count; ++i)
                {
                    //world.eventHandler.ApplyRequest(request, userRequest.credentials.username);
                    player.mostRecentRequest++;
                }
            }
            else
            {
                Console.WriteLine("Queueing (Invalid credentials) Log out request to client");
                //world.responseHandler.QueueLogout(userRequest.credentials.username); ???
            }

        }

    }
}
