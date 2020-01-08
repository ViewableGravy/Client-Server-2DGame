using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Script.Serialization;

namespace _2DGameServer
{
    public static class ReponseHandler
    {
        public static void UpdateClient(string username, World world)
        {
            //send anything that was updated

            /*
            JavaScriptSerializer serializer = new JavaScriptSerializer();

            Player player = world.GetUser(username);

            Modifications mods = player.UpdateManager.GetChanges(this);

            string message = serializer.Serialize(mods);

            Sender.UpdateClient(player, message);

    */
        }
    }
}
