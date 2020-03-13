using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MonoGame.Extended.Tiled.Renderers;
using Adventum.Entities;
using Adventum.Entities.Mobs;
using Adventum.Entities.Particles;
using Adventum.Util;
using Adventum.Core;
using Adventum.Core.IO;
using Adventum.GameStates;
using Adventum.Core.Resource;
using Adventum.UI;
using Adventum.Data;
using GeonBit.UI;
using MonoGame.Extended.Timers;

namespace Adventum.World
{
	public class GameWorld : GameState
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

		public static CountdownTimer deathTimer;

		public static TiledMapRenderer mapRenderer;


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
				}

				
				currentActiveControl = value;
			}
		}
		private static GeonBit.UI.Entities.Entity currentActiveControl = null;

		private static Dictionary<string, Level> levelCache;



		public GameWorld() : base(new GameplayScreen())
		{
			random = new Random();            
			input = new Input();          

			levelCache = new Dictionary<string, Level>();
			
			PlayerEntity playerEntity = new PlayerEntity(new Vector2(0f));
			playerEntity.input = input;
			player = new Player(this, input)
			{
				player = playerEntity
			};


			LoadLevel(startingLevel, false, playerEntity);
		}


		public override void Update(DeltaTime delta)
		{
			base.Update(delta);

			deltaTime = delta;

			mapRenderer.Update(delta);

			
			input.Update(!(UserInterface.Active.ActiveEntity is GeonBit.UI.Entities.RootPanel));

			if (input.KeyCheckPressed(Core.IO.MouseButton.Right))
				CurrentActiveControl = null;

			player.Update(delta);

			level.Update(delta);

			if (PlayerExists)
			{
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


		public override void Draw(SpriteBatch spriteBatch)
		{
			base.Draw(spriteBatch);

			mapRenderer.Draw(Main.Camera.GetViewMatrix());
			EntityManager.Draw(spriteBatch);
			//spriteBatch.Draw(ResourceManager.GetTexture("pixel"), input.MouseWorldPosition, color: Color.Red, layerDepth: 1f);
		}


		public void DrawLight(SpriteBatch spriteBatch, Texture2D lightMask)
		{
			EntityManager.DrawLight(spriteBatch, lightMask);
		}



		public  static void LoadLevel(string levelName, bool cacheLevel, params Entity[] toAdd)
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
