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
		private const string startingLevel = "beach";


		public static Random random;

		
		public static DeltaTime deltaTime;

		public static Player player;
		public static Input input;


		public static Level level;

		public static EntityManager EntityManager
		{
			get
			{
				return level.entityManager;
			}
		}

		
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
					//Audio.Play("closeTextbox");
				}

				
				currentActiveControl = value;
			}
		}
		private static GeonBit.UI.Entities.Entity currentActiveControl = null;

		private Dictionary<string, Level> levelCache;



		public GameWorld()
		{
			random = new Random();            
			input = new Input();          
			UserInterface.Active.AddEntity(gameplayScreen);

			levelCache = new Dictionary<string, Level>();
			
			PlayerEntity playerEntity = new PlayerEntity(new Vector2(0f));
			playerEntity.input = input;
			player = new Player(this, input)
			{
				player = playerEntity
			};


			LoadLevel(startingLevel, false, playerEntity);
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

			level.Update(delta);

			if (PlayerExists)
			{
				Main.DebugAdd(PlayerMob.Position.ToString(), "Player Position:");
				Main.Camera.LookAt(PlayerMob.Position);
			}
			else
			{
				deathTimer.Update(deltaTime);

				if (deathTimer.TimeRemaining.TotalSeconds <= 0)
				{
					player.player = new PlayerEntity(new Vector2())
					{
						input = GameWorld.input
					};
					LoadLevel(level.name, false, PlayerMob);
				}
			}
		}


		public void Draw(SpriteBatch spriteBatch)
		{
			mapRenderer.Draw(Main.Camera.GetViewMatrix());
			EntityManager.Draw(spriteBatch);
			//spriteBatch.Draw(ResourceManager.GetTexture("pixel"), input.MouseWorldPosition, color: Color.Red, layerDepth: 1f);
		}


		public void DrawLight(SpriteBatch spriteBatch, Texture2D lightMask)
		{
			EntityManager.DrawLight(spriteBatch, lightMask);
		}



		public void LoadLevel(string levelName, bool cacheLevel, params Entity[] toAdd)
		{
			if (cacheLevel)
			{
				levelCache[level.name] = level;
				level.Cache();
			}

			if (levelCache.ContainsKey(levelName))
			{
				level = levelCache[levelName];
			}
			else
			{
				level = new Level(levelName, toAdd);
			}

			level.Load();

			mapRenderer = new TiledMapRenderer(Main.graphics.GraphicsDevice, level.Map);

			deathTimer = new CountdownTimer(5);
		}


		public static bool EntityExists(Entity entity)
		{
			return EntityManager.EntityExists(entity);
		}


		public static void SpawnParticles(int amount, string particle, Vector2 position)
		{
			ParticleEffect effect = ResourceManager.GetParticle(particle);

			for (int i = 0; i < amount; i++)
			{
				Particle p = Particle.GenerateFromEffect(effect, position);
				EntityManager.CreateEntity(p);
			}
		}
	}
}
