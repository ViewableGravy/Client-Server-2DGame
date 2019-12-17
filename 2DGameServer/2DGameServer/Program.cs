using System;
using System.Timers;
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
        private static Queue<string> requests = new Queue<string>();
        private TcpListener tcpListener;
        private static List<Player> onlineUsers = new List<Player>();

       
        
        static void Main(string[] args)
        {
            

            Program program = new Program();
            program.StartServer();

            onlineUsers.Add(new Player("ViewableGravy", null));

            Timer timer = new Timer();
            timer.Elapsed += new ElapsedEventHandler(GameLogic);
            timer.Interval = 5000;
            timer.Enabled = true;

            //continue program
            while (true) { System.Threading.Thread.Sleep(100000); }
        }

        private static void GameLogic(object source, ElapsedEventArgs e)
        {
            Console.WriteLine("The Elapsed event was raised at {0:HH:mm:ss.fff}", e.SignalTime);
            
            //game logic
            while(requests.Count() != 0)
            {
                //Console.WriteLine("requests in queue: " + requests.Count());
                string request = requests.Dequeue();
                RequestHandler.HandleRequest(request);
                ReponseHandler.UpdateClient("ViewableGravy");
            }
        }


        /// <summary>
        /// Create tcpListener listening on correct port for any ip address
        /// </summary>
        /// <returns></returns>
        private bool StartServer()
        {
            const int PORT = 5678;

            try
            {
                tcpListener = new TcpListener(IPAddress.Any, PORT);
                tcpListener.Start();
                tcpListener.BeginAcceptTcpClient(new AsyncCallback(this.ProcessEvents), tcpListener);

                Console.WriteLine("Listing at Port {0}.", PORT);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
                return false;
            }

            return true;
        }
        private void ProcessEvents(IAsyncResult asyn)
        {
            try
            {
                

                TcpListener processListen = (TcpListener)asyn.AsyncState;
                TcpClient tcpClient = processListen.EndAcceptTcpClient(asyn);
                NetworkStream myStream = tcpClient.GetStream();
                if (myStream.CanRead)
                {
                    StreamReader readerStream = new StreamReader(myStream);
                    string request = readerStream.ReadToEnd();

                    // What happens with the input string?
                    {

                        requests.Enqueue(request);


                    }

                    readerStream.Close();
                }
                myStream.Close();
                tcpClient.Close();
                tcpListener.BeginAcceptTcpClient(new AsyncCallback(this.ProcessEvents), tcpListener);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
            }
        }

    }
}
