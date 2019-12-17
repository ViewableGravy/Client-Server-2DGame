using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.Net.Sockets;
using System.IO;
using System.Threading;

namespace _2DGameServer
{

    public class UDPListener
    {

        private const int PORT = 1234;
        private volatile bool isListening;
        Queue<string> requests;

        Thread listeningThread;


        public UDPListener(ref Queue<string> requests)
        {
            this.requests = requests;
            this.isListening = false;
        }

        public void StartListener()
        {
            if (!this.isListening)
            {
                listeningThread = new Thread(ListenForUDPPackages);
                this.isListening = true;
                listeningThread.Start();
            }
        }

        public void ListenForUDPPackages()
        {
            UdpClient listener = null;
            try
            {
                listener = new UdpClient(PORT);
            }
            catch (SocketException)
            {
                //do nothing
            }

            if (listener != null)
            {
                IPEndPoint groupEP = new IPEndPoint(IPAddress.Any, PORT);

                try
                {
                    while (this.isListening)
                    {
                        //Console.WriteLine("Waiting for UDP broadcast to port " + PORT);
                        byte[] bytes = listener.Receive(ref groupEP);
                        requests.Enqueue(Encoding.ASCII.GetString(bytes, 0, bytes.Length));
                    }
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.ToString());
                }
                finally
                {
                    listener.Close();
                    Console.WriteLine("Done listening for UDP broadcast");
                }
            }
        }

        public void StopListener()
        {
            this.isListening = false;
        }
    }
}
