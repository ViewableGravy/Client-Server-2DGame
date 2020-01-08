using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;
using System.Net.Sockets;
using System.IO;

namespace _2DGameServer
{

    public class World
    {
        
        private List<Player> onlineUsers = new List<Player>();
        private GameLogic logic;
        private UDPListener listener;
        public Queue<string> requests = new Queue<string>();

        EventHandler eventManager;
        //WorldObjectFactory woFactory;
        //OverlayFactory oFactory;
        //OverlayManager overlayManager;
        EventHandler eventHandler;
        Player player;

        static void Main(string[] args)
        {

            World world = new World();

            world.logic = new GameLogic(ref world.requests, ref world);
            world.listener = new UDPListener(ref world.requests);
            world.listener.StartListener();

            world.onlineUsers.Add(new Player(new SessionCredentials("7c9e6679-7425-40de-944b-e07fc1f90ae7", "ViewableGravy", "Test"), null));

            const int MILLIS = 5000;
            //Update server every MILLIS
            while (true) 
            {
                Console.WriteLine("Executing Game logic after {0} milliseconds", MILLIS);
                world.logic.execute();
                System.Threading.Thread.Sleep(MILLIS); 
            }
        }

        public Player GetPlayer(string username)
        {
            foreach(Player plr in onlineUsers)
                if (plr.credentials.username == username)
                    return plr;
            return null;
        }

        public bool ValidateUser(SessionCredentials credentials)
        {
            foreach (Player plr in onlineUsers)
            {
                if (plr.credentials.username == credentials.username)
                    return plr.credentials.SessionToken == credentials.SessionToken;
            }
            return false;
        }


    }
}
