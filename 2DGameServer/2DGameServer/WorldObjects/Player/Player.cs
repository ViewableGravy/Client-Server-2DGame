using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;

namespace _2DGameServer
{
    public class Player : WorldObject
    {
        public readonly SessionCredentials credentials;
        public readonly IPAddress iPAddress;
        public UpdateManager updateManager;
        public int mostRecentRequest = 0;

        /// <summary>
        /// Takes an IPAddress and Credentials for a client to modify this player. these should be established during
        /// the TCP login process and passed when creating a player.
        /// </summary>
        /// <param name="credentials"></param>
        /// <param name="iPAddress"></param>
        public Player(SessionCredentials credentials, IPAddress iPAddress, World world, int startingRequest)
        {
            this.credentials = credentials;
            this.iPAddress = iPAddress;
            updateManager = new UpdateManager(this, world);
            mostRecentRequest = startingRequest;
        }

    }
}
