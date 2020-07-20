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

        private static readonly Dictionary<string, EntityLoader> loaders = BuildLoaders();

        private static Random random = new Random();

        private static Level level;



        private static Dictionary<string, EntityLoader> BuildLoaders()
        {
            var d = new Dictionary<string, EntityLoader>();

            Vector2 offset = new Vector2(16, 0);
            d["Entity"] = (TiledMapTileObject t) => new Mob(t.Position + offset);
            d["Zombie"] = (TiledMapTileObject t) => new Zombie(t.Position + offset);
            d["Reaper"] = (TiledMapTileObject t) => new Reaper(t.Position + offset);
            d["Tree"] = (TiledMapTileObject t) => new Tree(t.Position + offset, bool.Parse(t.Properties["Collision"]));
            d["Chair"] = (TiledMapTileObject t) => new Furniture(t.Position + offset, "Chair", facing: (Direction)int.Parse(t.Properties["Direction"]));
            d["Torch"] = (TiledMapTileObject t) => new Torch(t.Position + offset, new Color(uint.Parse(t.Properties["Color"].Replace("#", ""), System.Globalization.NumberStyles.HexNumber)),
                int.Parse(t.Properties["Radius"]));
            d["LightEmitter"] = (TiledMapTileObject t) => new Torch(t.Position + offset, new Color(uint.Parse(t.Properties["Color"].Replace("#", ""), System.Globalization.NumberStyles.HexNumber)),
                int.Parse(t.Properties["Radius"]), false);
            d["Sign"] = (TiledMapTileObject t) => new Sign(t.Position + offset, t.Properties["Title"], t.Properties["Text"], int.Parse(t.Properties["Skin"]));
            d["Table"] = (TiledMapTileObject t) => new Furniture(t.Position + offset, "table", "Table", 45, lightRadius: 50)
            {
                collisionType = CollisionType.Stationary
            };
			d["Slime"] = (TiledMapTileObject t) => new Slime(t.Position + offset);
			d["Door"] = (TiledMapTileObject t) => new Door(t.Position + offset, t.Properties["Level"], t.Properties["Cache Level"] == "true");
            d["Chest"] = (TiledMapTileObject t) =>
            {
                return new Chest(t.Position + offset, t.Properties["LootTable"]);
            };
            d["Boss"] = (TiledMapTileObject t) => new SlimeBoss(t.Position + offset);
            d["KeyDoor"] = (TiledMapTileObject t) => new KeyDoor(t.Position + offset, t.Properties["Level"], t.Properties["Cache Level"] == "true");
            d["Skeleton"] = (TiledMapTileObject t) => new Skeleton(t.Position + offset);
            d["Bush"] = (TiledMapTileObject t) => new Bush(t.Position + offset);
            d["BigTree"] = (TiledMapTileObject t) => new BigTree(t.Position + offset, bool.Parse(t.Properties["Collision"]));

            return d;
        }


        public static void LoadMapObjects(Level level, bool cached = false)
        {
            MapHandler.level = level;
            TiledMap map = level.Map;

            TiledMapObjectLayer collisionLayer = map.GetLayer<TiledMapObjectLayer>("Collision");
            TiledMapObjectLayer entityLayer = map.GetLayer<TiledMapObjectLayer>("Entity");
            TiledMapObjectLayer misicLayer = map.GetLayer<TiledMapObjectLayer>("Misic");

            if (!cached)
            {
                foreach (TiledMapRectangleObject o in collisionLayer.Objects)
                {
                    LoadCollisionObject(o);
                }

                foreach (TiledMapTileObject o in entityLayer.Objects)
                {
                    LoadEntityObject(o);
                }
            }

            foreach (TiledMapObject o in misicLayer.Objects)
            {
                LoadMisicObject(o);
            }


            GameWorld.self.LightColor = new Color(uint.Parse(misicLayer.Properties["Light"].Replace("#",""), System.Globalization.NumberStyles.HexNumber));
        }

        private static void LoadCollisionObject(TiledMapRectangleObject rectangle)
        {
            WallColider collider = new WallColider(rectangle.Position.ToPoint(), new Point((int)rectangle.Size.Width, (int)rectangle.Size.Height));
            level.collisionManager.AddCollider(collider);
        }


        private static void LoadEntityObject(TiledMapTileObject entity)
        {
            if (loaders.ContainsKey(entity.Type))
                level.entityManager.CreateEntity(loaders[entity.Type].Invoke(entity));
        }

        private static void LoadMisicObject(TiledMapObject obj)
        {
            if (obj.Type == "PlayerStart")
                GameWorld.player.player.Position = obj.Position;

			else if ((obj.Type == "Thought" || (obj.Type == "Tutorial" && GameWorld.showTutorial) )  && obj is TiledMapRectangleObject)
			{
				var rObj = (TiledMapRectangleObject)obj;
				level.entityManager.CreateEntity(new Thought(new Rectangle(rObj.Position.ToPoint(), new Point((int)rObj.Size.Width,
					(int)rObj.Size.Height)), rObj.Properties["Title"], rObj.Properties["Text"], int.Parse(rObj.Properties["Skin"])));
			}
        }
    }
}
