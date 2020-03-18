using Adventum.Core.Resource;
using Adventum.Core.Collision;
using Adventum.Core;
using Adventum.Util;
using Adventum.Entities;
using Microsoft.Xna.Framework;
using MonoGame.Extended.Tiled;

namespace Adventum.World
{
	public class Level
	{
		public EntityManager entityManager;
		public CollisionManager collisionManager;

		public TiledMap Map { get; private set; }
		public bool cached = false;
		public string name = "";

		private Entity[] entitiesToAdd;


		public Level(string levelName, Entity[] entitiesToAdd)
		{
			this.name = levelName;

			entityManager = new EntityManager();
			collisionManager = new CollisionManager();

			this.entitiesToAdd = entitiesToAdd;
		}


		public void Load()
		{
			Load(entitiesToAdd);
		}
		public void Load(Entity[] toAdd)
		{
			Map = ResourceManager.GetMap(name);

			MapHandler.LoadMapObjects(this, cached);

			if (!cached)
			{
				foreach(Entity e in toAdd)
				{
					entityManager.CreateEntity(e);
				}
			}
			cached = false;
		}

		public void Cache()
		{
			Map = null;
			cached = true;
		}


		public void Update(DeltaTime delta)
		{
			entityManager.Update(delta);
			collisionManager.Update(delta);
		}
	}
}
