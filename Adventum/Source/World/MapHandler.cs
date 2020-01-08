using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using MonoGame.Extended;
using MonoGame.Extended.Tiled;
using Adventum.Core.Collision;
using Adventum.Entities;
using Adventum.Entities.Mobs;
using Adventum.Entities.Decor;
using Adventum.Data;

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
            d[3] = (TiledMapTileObject t) => new Tree(t.Position);
            d[4] = (TiledMapTileObject t) => new Furniture(t.Position, "Chair", (Direction)Int32.Parse(t.Properties["Direction"]));

            return d;
        }


        public static void LoadMapObjects(TiledMap map)
        {
            TiledMapObjectLayer collisionLayer = map.GetLayer<TiledMapObjectLayer>("Collision");
            TiledMapObjectLayer entityLayer = map.GetLayer<TiledMapObjectLayer>("Entity");
            TiledMapObjectLayer misicLayer = map.GetLayer<TiledMapObjectLayer>("Misic");

            foreach (TiledMapRectangleObject o in collisionLayer.Objects)
            {
                LoadCollisionObject(o);
            }

            foreach (TiledMapTileObject o in entityLayer.Objects)
            {
                LoadEntityObject(o);
            }

            foreach (TiledMapObject o in misicLayer.Objects)
            {
                LoadMisicObject(o);
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

        private static void LoadMisicObject(TiledMapObject obj)
        {
            if (obj.Name == "Player Start")
                GameWorld.player.player.Position = obj.Position;
        }
    }
}
