using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _2DGameServer
{
    public class Modifications
    {
        public List<WorldObject> update;
        public List<WorldObject> create;
        public List<WorldObject> remove;

        public Modifications(List<WorldObject> toUpdate, List<WorldObject> toCreate, List<WorldObject> toRemove)
        {
            update = toUpdate;
            create = toCreate;
            remove = toRemove;
        }
    }
}
