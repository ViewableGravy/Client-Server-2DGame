using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;
using System.Net.Sockets;
using System.IO;

namespace _2DGameServer
{

    class Program
    {
        
        private static List<Player> onlineUsers = new List<Player>();
        private static GameLogic logic;
        private static UDPListener listener;
        public Queue<string> requests = new Queue<string>();

        static void Main(string[] args)
        {

            Program program = new Program();

            logic = new GameLogic(ref program.requests);
            listener = new UDPListener(ref program.requests);
            listener.StartListener();

            onlineUsers.Add(new Player("ViewableGravy", null));

            const int MILLIS = 5000;
            //Update server every MILLIS
            while (true) 
            {
                Console.WriteLine("Executing Game logic after {0} milliseconds", MILLIS);
                logic.execute();
                System.Threading.Thread.Sleep(MILLIS); 
            }
        }


    }
}
