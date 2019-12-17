using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _2DGameServer
{
    class GameLogic
    {
        Queue<string> requests;

        public GameLogic(ref Queue<string> requests)
        {
            this.requests = requests;
        }

        public void execute() 
        {
            Console.WriteLine("Executing {0} commands", requests.Count());

            //game logic
            while (requests.Count() != 0)
            {
                //Console.WriteLine("requests in queue: " + requests.Count());
                string request = requests.Dequeue();
                //RequestHandler.HandleRequest(request);
                ReponseHandler.UpdateClient("ViewableGravy");
            }
        }
    }
}
