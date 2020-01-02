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
        private delegate Entity EntityLoader(TiledMapTileObject mapObject);

        private static Dictionary<int, EntityLoader> loaders = BuildLoaders();


        private static Random random = new Random();



        private static Dictionary<int, EntityLoader> BuildLoaders()
        {
            var d = new Dictionary<int, EntityLoader>();

            d[0] = (TiledMapTileObject t) => new Mob(t.Position);
            d[1] = (TiledMapTileObject t) => new Zombie(t.Position);
            d[2] = (TiledMapTileObject t) => new Reaper(t.Position);

            return d;
        }


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

        private static void LoadCollisionObject(TiledMapRectangleObject rectangle)
        {
            WallColider collider = new WallColider(rectangle.Position.ToPoint(), new Point((int)rectangle.Size.Width, (int)rectangle.Size.Height));
            GameWorld.collisionManager.AddCollider(collider);
        }


        private static void LoadEntityObject(TiledMapTileObject entity)
        {
            TiledMapTilesetTile tile = entity.Tile;
            if (tile != null)
                GameWorld.entityManager.CreateEntity(loaders[tile.LocalTileIdentifier - 1].Invoke(entity));
        }
    }
}
