using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MonoGame.Extended.Collisions;
using Adventum.Source.Entities;
using Adventum.Source.Util;
using Adventum.Source.Core.IO;

namespace Adventum.Source.Core
{
    public class World
    {
        public static EntityManager entityManager;
        public static CollisionWorld collisionWorld;

        public Player player;
        public Input input;

        public World()
        {
            entityManager = new EntityManager();
            collisionWorld = new CollisionWorld(new Vector2());
            collisionWorld.CreateGrid(new int[1000], 30, 30, 32, 32);

            input = new Input();

            Entity playerEntity = entityManager.CreateEntity(new Entity(new Vector2(12f)));
            entityManager.CreateEntity(new Entity(new Vector2(20f)));
            player = new Player(this, input);
            player.player = playerEntity;
        }


        public void Update(GameTime gameTime)
        {
            DeltaTime delta = new DeltaTime(gameTime.TotalGameTime, gameTime.ElapsedGameTime);

            entityManager.Update(delta);
            collisionWorld.Update(gameTime);

            input.Update();
            player.Update(delta);
        }


        public void Draw(SpriteBatch spriteBatch)
        {
            entityManager.Draw(spriteBatch);
        }


        public bool EntityExists(Entity entity)
        {
            return entityManager.EntityExists(entity);
        }
    }
}
