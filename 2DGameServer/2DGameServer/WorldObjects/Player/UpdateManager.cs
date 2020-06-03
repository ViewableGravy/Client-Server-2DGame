using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _2DGameServer
{
    /// <summary>
    /// A manager for the player to organise objects that it can interact with
    /// </summary>
    public class UpdateManager
    {
        //maybe use a collection that allows for getting objects based on direction/distance for efficiency
        List<WorldObject> updated = new List<WorldObject>();

        //temp
        public List<WorldObject> alreadyExist = new List<WorldObject>();
        List<WorldObject> newObjects = new List<WorldObject>();
        List<WorldObject> removed = new List<WorldObject>();

        Player player;
        World world;

        public UpdateManager(Player myPlayer, World world)
        {
            player = myPlayer;
            this.world = world;
        }

        public void Update(WorldObject sender)
        {
            updated.Add(sender);
        }

        public Modifications GetChanges()
        {
            Modifications mods = new Modifications(updated, newObjects, removed);

            alreadyExist.AddRange(newObjects);
            newObjects.Clear();
            removed.Clear();
            updated.Clear();

            return mods;
        }

        /// <summary>
        /// Query quad tree for new objects and then apply modifications to class. Must be run before GetChanges.
        /// note: world can be replaced with quadtree once it is implemented.
        /// The purpose of this function is to Refresh the player update manager so that the user can recieve the latest updated information
        /// </summary>
        /// <param name="world"></param>
        public void PrepareClientUpdate(World world)
        {
            //List<WorldObject> context = QuadTree.Query(LinearIncreaseBoundingBox(player.ViewRect, 100));

            List<WorldObject> context = alreadyExist;

            //remove client existing objects from all server nearby objects and then the remain is the new objects for client
            var tempNewObjects = context.Except(alreadyExist);
            foreach (WorldObject obj in tempNewObjects) {
                obj.Subscribe(player);
                newObjects.Add(obj);
            }

            //whats in context but not alreadyExist
            var toRemove = alreadyExist.Except(context);
            foreach (WorldObject obj in toRemove) {
                obj.Unsubscribe(player);
                alreadyExist.Remove(obj);
                removed.Add(obj);
            }
              
             
        }
    }
}
