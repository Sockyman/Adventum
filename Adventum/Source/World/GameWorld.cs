using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using MonoGame.Extended;
using MonoGame.Extended.Tiled;
using MonoGame.Extended.Tiled.Renderers;
using Adventum.Entities;
using Adventum.Entities.Mobs;
using Adventum.Util;
using Adventum.Core;
using Adventum.Core.IO;
using Adventum.Core.Collision;
using Adventum.Core.Resource;
using Adventum.Data;

namespace Adventum.World
{
    public class GameWorld
    {
        public static Random random;

        public static EntityManager entityManager;
        public static CollisionManager collisionManager;
        public static DeltaTime deltaTime;

        public static Player player;
        public static Input input;

        public static TiledMap Map { get; private set; }
        public TiledMapRenderer mapRenderer;


        public GameWorld()
        {
            random = new Random();

            collisionManager = new CollisionManager();
            entityManager = new EntityManager();
            input = new Input();

            Map = ResourceManager.GetMap("TestMap");
            mapRenderer = new TiledMapRenderer(Main.graphics.GraphicsDevice, Map);
            MapHandler.LoadMapObjects(Map);

            
            {
                PlayerEntity playerEntity = (PlayerEntity)entityManager.CreateEntity(new PlayerEntity(new Vector2(0f)));
                playerEntity.input = input;
                player = new Player(this, input)
                {
                    player = playerEntity
                };

                /*for (int i = 0; i < 0; i++)
                    entityManager.CreateEntity(new Enemy(new Vector2(random.Next(640), random.Next(360))));*/
            }
        }


        public void Update(GameTime gameTime)
        {
            DeltaTime delta = new DeltaTime(gameTime.TotalGameTime, gameTime.ElapsedGameTime);
            deltaTime = delta;

            mapRenderer.Update(gameTime);

            input.Update();
            player.Update(delta);

            entityManager.Update(delta);
            collisionManager.Update(delta);

            if (EntityExists(player.player))
                Main.Camera.LookAt(player.player.Position);

            //Main.Camera.Rotate(0.001f);


            if (input.KeyCheckPressed(Keys.F11))
            {
                Main.graphics.IsFullScreen = !Main.graphics.IsFullScreen;
                Main.graphics.ApplyChanges();
            }
        }


        public void Draw(SpriteBatch spriteBatch)
        {
            mapRenderer.Draw(Main.Camera.GetViewMatrix());
            entityManager.Draw(spriteBatch);
        }


        public static bool EntityExists(Entity entity)
        {
            return entityManager.EntityExists(entity);
        }
    }
}
