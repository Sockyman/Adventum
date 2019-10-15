using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Adventum.Source.Entities;
using Adventum.Source.Util;
using Adventum.Source.Core.IO;

namespace Adventum.Source.Core
{
    public class World
    {
        public static Random random;

        public static EntityManager entityManager;

        public Player player;
        public Input input;

        public World()
        {
            random = new Random();

            entityManager = new EntityManager();
            input = new Input();


            {
                Entity playerEntity = entityManager.CreateEntity(new Entity(new Vector2(200f)));
                player = new Player(this, input);
                player.player = playerEntity;

                for (int i = 0; i < 10000; i++)
                {
                    entityManager.CreateEntity(new Entity(new Vector2(random.Next(640), random.Next(360))));
                }
            }
        }


        public void Update(GameTime gameTime)
        {
            DeltaTime delta = new DeltaTime(gameTime.TotalGameTime, gameTime.ElapsedGameTime);

            entityManager.Update(delta);

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
