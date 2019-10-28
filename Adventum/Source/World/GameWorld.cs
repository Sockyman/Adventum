using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Adventum.Source.Entities;
using Adventum.Source.Util;
using Adventum.Source.Core;
using Adventum.Source.Core.IO;
using Adventum.Source.Core.Collision;

namespace Adventum.Source.World
{
    public class GameWorld
    {
        public static Random random;

        public static EntityManager entityManager;
        public static CollisionManager collisionManager;

        public Player player;
        public Input input;

        public GameWorld()
        {
            random = new Random();

            collisionManager = new CollisionManager();
            entityManager = new EntityManager();
            input = new Input();


            {
                Mob playerEntity = (Mob)entityManager.CreateEntity(new Mob(new Vector2(200f)));
                player = new Player(this, input);
                player.player = playerEntity;

                entityManager.CreateEntity(new Entity(new Vector2(random.Next(400), random.Next(300))));
            }
        }


        public void Update(GameTime gameTime)
        {
            DeltaTime delta = new DeltaTime(gameTime.TotalGameTime, gameTime.ElapsedGameTime);

            entityManager.Update(delta);
            collisionManager.Update(delta);

            input.Update();
            player.Update(delta);

            if (input.KeyCheckPressed(Keys.F11))
            {
                Main.graphics.IsFullScreen = !Main.graphics.IsFullScreen;
                Main.graphics.ApplyChanges();
            }
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
