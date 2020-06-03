using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _2DGameServer
{
    public class WorldObject
    {
        List<Player> nearbyPlayers;

        public WorldObject()
        {
            nearbyPlayers = new List<Player>(); ;
        }

        public void Subscribe(Player player)
        {
            nearbyPlayers.Add(player);
        }

        public void Unsubscribe(Player player)
        {
            nearbyPlayers.Remove(player);
        }
    }
}
