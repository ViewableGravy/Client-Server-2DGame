using Newtonsoft.Json;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;

namespace _2DGameServer
{

    public class UDPListener
    {

        private const int PORT = 1234;
        private volatile bool isListening;
        private ConcurrentQueue<UserRequest> queue = new ConcurrentQueue<UserRequest>();

        Thread listeningThread;


        public UDPListener()
        {
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

        private void ListenForUDPPackages()
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
                        //Consider changing requests to sort incoming messages by time sent rather than time arrived
                        byte[] bytes = listener.Receive(ref groupEP);
                        ThreadPool.QueueUserWorkItem(_ =>
                        {
                            queue.Enqueue(JsonConvert.DeserializeObject<UserRequest>(Encoding.ASCII.GetString(bytes, 0, bytes.Length)));
                        });
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

        public Queue<UserRequest> GetQueueClone()
        {
            var newQueue = new Queue<UserRequest>();
            while (queue.TryDequeue(out var temp)) newQueue.Enqueue(temp);
            return newQueue;
        }

        public void StopListener()
        {
            this.isListening = false;
        }
    }
}
