using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;

namespace _2DGameServer
{
    class Player
    {
        public string username;
        public IPAddress iPAddress;

        public Player(string username, IPAddress iPAddress)
        {
            this.username = username;
            this.iPAddress = iPAddress;
        }
    }
}
