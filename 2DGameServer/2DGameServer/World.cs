using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Net;
using System.Threading;
using System.Threading.Tasks;

namespace _2DGameServer
{

    public class World
    {

        public List<Player> onlineUsers = new List<Player>();
        private UDPListener listener;
        private ResponseHandler responseHandler = new ResponseHandler();
        private RequestHandler requestHandler;


        EventHandler eventManager;
        //WorldObjectFactory woFactory;
        //OverlayFactory oFactory;
        //OverlayManager overlayManager;
        EventHandler eventHandler;
        Player player;

        static void Main(string[] args)
        {

            World world = new World();

            world.requestHandler = new RequestHandler(ref world);
            world.listener = new UDPListener();
            world.listener.StartListener();

            world.onlineUsers.Add(new Player(new SessionCredentials("7c9e6679-7425-40de-944b-e07fc1f90ae7",
                                                                    "ViewableGravy",
                                                                    "Test"), Dns.GetHostByName(Dns.GetHostName()).AddressList[0], world, 0));
            world.onlineUsers[0].updateManager.alreadyExist.Add(new WorldObject());
            //note: this is a test player, when the client originally logs in, the UUID and Ipaddress of the user will be send
            //with their login credentials

            //const int MILLIS = 1000;
            const int MILLIS = 1000;
            while (true)
            {
                ThreadPool.QueueUserWorkItem(foo =>
                {
                    Console.WriteLine("New: ");

                    // console.writeline literally takes ages (1ms);
                    world.TestExecutionTime(true, () =>
                    {
                        var userRequests = world.listener.GetQueueClone();

                        while (userRequests.Count != 0)
                            world.requestHandler.HandleRequest(userRequests.Dequeue());
                    });


                    //update the world
                    //world.Update()

                    //Update Client for each user
                    ThreadPool.QueueUserWorkItem(_ => world.UpdateClients());
                });
                Thread.Sleep(MILLIS);
            }
        }

        public void TestExecutionTime(bool run, Action method)
        {
            var stopwatch = Stopwatch.StartNew();

            method();

            stopwatch.Stop();

            if (run)
            {
                Console.WriteLine("test execution on method. ms: {0}, mms: {1}",
                    (int)stopwatch.Elapsed.TotalMilliseconds,
                    (int)((stopwatch.Elapsed.TotalMilliseconds - ((int)stopwatch.Elapsed.TotalMilliseconds)) * 1000));
            }
        }

        public void UpdateClients()
        {
            Parallel.ForEach(onlineUsers, player => responseHandler.UpdateClient(player));
        }

        public Player GetUser(string username)
        {
            foreach (Player plr in onlineUsers)
                if (plr.credentials.username == username)
                    return plr;
            return null;
        }

        public bool ValidateUser(SessionCredentials credentials)
        {
            return onlineUsers.Exists(player =>
            {
                return player.credentials.username == credentials.username && player.credentials.SessionToken == credentials.SessionToken;
            });
        }


    }
}
