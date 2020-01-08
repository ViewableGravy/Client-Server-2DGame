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
        public UserDetails details;
        public IPAddress iPAddress;
        public Guid guid;

        public Player(UserDetails details, IPAddress iPAddress)
        {
            this.details = details;
            this.iPAddress = iPAddress;
        }
    }
}
