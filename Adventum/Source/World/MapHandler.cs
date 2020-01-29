using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using MonoGame.Extended;
using MonoGame.Extended.Tiled;
using Adventum.Core.Collision;
using Adventum.Entities;
using Adventum.Entities.Mobs;
using Adventum.Entities.Decor;
using Adventum.Entities.Interaction;
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

            Vector2 offset = new Vector2(16, 0);
            d[0] = (TiledMapTileObject t) => new Mob(t.Position + offset);
            d[1] = (TiledMapTileObject t) => new Zombie(t.Position + offset);
            d[2] = (TiledMapTileObject t) => new Reaper(t.Position + offset);
            d[3] = (TiledMapTileObject t) => new Tree(t.Position + offset);
            d[4] = (TiledMapTileObject t) => new Furniture(t.Position + offset, "Chair", facing: (Direction)Int32.Parse(t.Properties["Direction"]));
            d[5] = (TiledMapTileObject t) => new Torch(t.Position + offset, new Color(uint.Parse(t.Properties["Color"].Replace("#", ""), System.Globalization.NumberStyles.HexNumber)),
                Int32.Parse(t.Properties["Radius"]));
            d[6] = (TiledMapTileObject t) => new Torch(t.Position + offset, new Color(uint.Parse(t.Properties["Color"].Replace("#", ""), System.Globalization.NumberStyles.HexNumber)),
                Int32.Parse(t.Properties["Radius"]), false);
            d[7] = (TiledMapTileObject t) => new Sign(t.Position + offset, t.Properties["Title"], t.Properties["Text"], int.Parse(t.Properties["Skin"]));
            d[8] = (TiledMapTileObject t) => new Furniture(t.Position + offset, "table", "Table", 45, lightRadius: 50);
			d[9] = (TiledMapTileObject t) => new Slime(t.Position + offset);
			d[10] = (TiledMapTileObject t) => new Door(t.Position + offset, t.Properties["Level"]);

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


            Main.LightColor = new Color(uint.Parse(misicLayer.Properties["Light"].Replace("#",""), System.Globalization.NumberStyles.HexNumber));
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

			if (obj.Type == "Thought" && obj is TiledMapRectangleObject)
			{
				var rObj = (TiledMapRectangleObject)obj;
				GameWorld.entityManager.CreateEntity(new Thought(new Rectangle(rObj.Position.ToPoint(), new Point((int)rObj.Size.Width,
					(int)rObj.Size.Height)), rObj.Properties["Title"], rObj.Properties["Text"], int.Parse(rObj.Properties["Skin"])));
			}
        }
    }
}
