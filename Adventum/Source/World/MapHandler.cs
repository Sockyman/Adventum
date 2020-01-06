using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using MonoGame.Extended;
using MonoGame.Extended.Tiled;
using Adventum.Core.Collision;
using Adventum.Entities;
using Adventum.Entities.Mobs;

namespace Adventum.World
{
    public static class MapHandler
    {
        public static Random random = new Random();


        public static void LoadMapObjects(TiledMap map)
        {
            TiledMapObjectLayer collisionLayer = map.GetLayer<TiledMapObjectLayer>("Collision");
            TiledMapObjectLayer entityLayer = map.GetLayer<TiledMapObjectLayer>("Entity");

            foreach (TiledMapRectangleObject o in collisionLayer.Objects)
            {
                LoadCollisionObject(o);
            }

            foreach (TiledMapTileObject o in entityLayer.Objects)
            {
                LoadEntityObject(o);
            }
        }

        public static void LoadCollisionObject(TiledMapRectangleObject rectangle)
        {
            WallColider collider = new WallColider(rectangle.Position.ToPoint(), new Point((int)rectangle.Size.Width, (int)rectangle.Size.Height));            
        }


        public static void LoadEntityObject(TiledMapTileObject entity)
        {
            if (random.Next(2) > 0)
                GameWorld.entityManager.CreateEntity(new Reaper(entity.Position));
            else
                GameWorld.entityManager.CreateEntity(new Zombie(entity.Position));
        }
    }
}
