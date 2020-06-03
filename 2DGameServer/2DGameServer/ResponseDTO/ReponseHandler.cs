using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Web.Script.Serialization;

namespace _2DGameServer
{
    public class ResponseHandler
    {
        public void UpdateClient(Player player)
        {
            
            //send anything that was updated
            const int PORT = 1011;
            var packet = new ServerClientPacket(player.updateManager.GetChanges(), player.mostRecentRequest);
           
            SendUDP(new JavaScriptSerializer().Serialize(packet), player.iPAddress, PORT);
        }

        private void SendUDP(string message, IPAddress ip, int port)
        {
            new World().TestExecutionTime(false,() =>
            {
                Socket sock = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.Udp);
                IPEndPoint address = new IPEndPoint(ip, port);
                sock.SendTo(Encoding.ASCII.GetBytes(message), address);
            });
        }
    }
}
