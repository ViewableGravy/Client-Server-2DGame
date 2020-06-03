using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _2DGameServer
{
    class ServerClientPacket
    {
        public Modifications modifications;
        public int mostRecentRequest;

        public ServerClientPacket(Modifications modifications, int mostRecentRequest)
        {
            this.modifications = modifications;
            this.mostRecentRequest = mostRecentRequest;
        }

    }
}
