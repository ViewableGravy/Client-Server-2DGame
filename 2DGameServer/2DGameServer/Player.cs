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
        public readonly SessionCredentials credentials;
        public readonly IPAddress iPAddress;

        /// <summary>
        /// Takes an IPAddress and Credentials for a client to modify this player. these should be established during
        /// the TCP login process and passed when creating a player.
        /// </summary>
        /// <param name="credentials"></param>
        /// <param name="iPAddress"></param>
        public Player(SessionCredentials credentials, IPAddress iPAddress)
        {
            this.credentials = credentials;
            this.iPAddress = iPAddress;
        }

    }
}
