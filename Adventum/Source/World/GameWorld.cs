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
using Adventum.UI;
using Adventum.Data;
using GeonBit.UI;

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
        public static PlayerEntity PlayerMob
        {
            get
            {
                return player.player;
            }
        }
        public static bool PlayerExists
        {
            get
            {
                return EntityExists(PlayerMob);
            }
        }

        public TiledMapRenderer mapRenderer;


        public GameWorld()
        {
            random = new Random();            
            input = new Input();          
            UserInterface.Active.AddEntity(new GameplayScreen());

            ClearManagers();
            
            PlayerEntity playerEntity = (PlayerEntity)entityManager.CreateEntity(new PlayerEntity(new Vector2(0f)));
            playerEntity.input = input;
            player = new Player(this, input)
            {
                player = playerEntity
            };



            LoadLevel("lvl1");
        }


        public void Update(GameTime gameTime)
        {
            DeltaTime delta = new DeltaTime(gameTime.TotalGameTime, gameTime.ElapsedGameTime);
            deltaTime = delta;

            mapRenderer.Update(gameTime);

            
            input.Update(!(UserInterface.Active.ActiveEntity is GeonBit.UI.Entities.RootPanel));

            player.Update(delta);

            entityManager.Update(delta);
            collisionManager.Update(delta);

            if (EntityExists(PlayerMob))
                Main.Camera.LookAt(PlayerMob.Position);

            //Main.Camera.Rotate(0.001f);


            
        }


        public void Draw(SpriteBatch spriteBatch)
        {
            mapRenderer.Draw(Main.Camera.GetViewMatrix());
            entityManager.Draw(spriteBatch);
            //mapRenderer.Draw(2, Main.Camera.GetViewMatrix(), depth: 0);
        }



        public void LoadLevel(string levelName)
        {
            ClearManagers(player.player);

            Map = ResourceManager.GetMap(levelName);
            mapRenderer = new TiledMapRenderer(Main.graphics.GraphicsDevice, Map);
            MapHandler.LoadMapObjects(Map);

            entityManager.CreateEntity(new Entities.Decor.Furniture(PlayerMob.Position, "chair"));
        }


        private void ClearManagers()
        {
            entityManager = new EntityManager();
            collisionManager = new CollisionManager();
        }
        private void ClearManagers(Mob toKeep)
        {
            ClearManagers();

            entityManager.CreateEntity(toKeep);
        }


        public static bool EntityExists(Entity entity)
        {
            return entityManager.EntityExists(entity);
        }
    }
}
