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
using Adventum.Entities.Particles;
using Adventum.Util;
using Adventum.Core;
using Adventum.Core.IO;
using Adventum.Core.Collision;
using Adventum.Core.Resource;
using Adventum.UI;
using Adventum.Data;
using GeonBit.UI;
using MonoGame.Extended.Timers;

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
		public static string levelName = "";
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

		public CountdownTimer deathTimer;

        public TiledMapRenderer mapRenderer;

        public static GameplayScreen gameplayScreen = new GameplayScreen();

        public static GeonBit.UI.Entities.Entity CurrentActiveControl
        {
            get
            {
                return currentActiveControl;
            }
            set
            {
                if (currentActiveControl != null)
                {
                    currentActiveControl.RemoveFromParent();
                    Audio.Play("closeTextbox");
                }

				
                currentActiveControl = value;
            }
        }
        private static GeonBit.UI.Entities.Entity currentActiveControl = null;


        public GameWorld()
        {
            random = new Random();            
            input = new Input();          
            UserInterface.Active.AddEntity(gameplayScreen);

            ClearManagers();
            
            PlayerEntity playerEntity = (PlayerEntity)entityManager.CreateEntity(new PlayerEntity(new Vector2(0f)));
            playerEntity.input = input;
            player = new Player(this, input)
            {
                player = playerEntity
            };



            LoadLevel("beach");


			TextBox tb = new TextBox("Welcome to Adventum", "Controls:\nWAS & D to move,\nLeft click to attack,\nRight click to read signs,\nRight click to close this menu",
				GeonBit.UI.Entities.PanelSkin.Fancy);

			gameplayScreen.AddChild(tb);
			//CurrentActiveControl = tb;
        }


        public void Update(GameTime gameTime)
        {
            DeltaTime delta = new DeltaTime(gameTime.TotalGameTime, gameTime.ElapsedGameTime);
            deltaTime = delta;

            mapRenderer.Update(gameTime);

            
            input.Update(!(UserInterface.Active.ActiveEntity is GeonBit.UI.Entities.RootPanel));

            if (input.KeyCheckPressed(Core.IO.MouseButton.Right))
                CurrentActiveControl = null;

            player.Update(delta);

            entityManager.Update(delta);
            collisionManager.Update(delta);

            if (EntityExists(PlayerMob))
                Main.Camera.LookAt(PlayerMob.Position);

            //Main.Camera.Rotate(0.001f);


            if (!PlayerExists)
			{
				deathTimer.Update(deltaTime);

				if (deathTimer.TimeRemaining.TotalSeconds <= 0)
				{
					player.player = new PlayerEntity(new Vector2())
					{
						input = GameWorld.input
					};
					LoadLevel(levelName);
				}
			}
        }


        public void Draw(SpriteBatch spriteBatch)
        {
            mapRenderer.Draw(Main.Camera.GetViewMatrix());
            entityManager.Draw(spriteBatch);
			spriteBatch.Draw(ResourceManager.GetTexture("pixel"), input.MouseWorldPosition, color: Color.Red, layerDepth: 1f);
		}


        public void DrawLight(SpriteBatch spriteBatch, Texture2D lightMask)
        {
            entityManager.DrawLight(spriteBatch, lightMask);
        }



        public void LoadLevel(string levelName)
        {
            ClearManagers(player.player);

            Map = ResourceManager.GetMap(levelName);
            mapRenderer = new TiledMapRenderer(Main.graphics.GraphicsDevice, Map);
            MapHandler.LoadMapObjects(Map);

			GameWorld.levelName = levelName;
        }


        private void ClearManagers()
        {
            entityManager = new EntityManager();
            collisionManager = new CollisionManager();
			deathTimer = new CountdownTimer(5);
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


		public static void SpawnParticles(int amount, string particle, Vector2 position)
		{
			ParticleEffect effect = ResourceManager.GetParticle(particle);

			for (int i = 0; i < amount; i++)
			{
				Particle p = Particle.GenerateFromEffect(effect, position);
				entityManager.CreateEntity(p);
			}
		}
    }
}
