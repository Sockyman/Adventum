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
using MonoGame.Extended;
using MonoGame.Extended.Timers;

namespace Adventum.World
{
	public class GameWorld : GameState
	{
		private const string startingLevel = "beach";

		public static GameWorld self;

		public static Random random;


		public static bool Paused { get; set; }

		
		public static DeltaTime deltaTime;

		public static Player player;
		public static Input input;

		public static bool showTutorial = Properties.Settings.Default.showTutorial;


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
		private static GeonBit.UI.Entities.Entity currentActiveControl;

		private static Dictionary<string, Level> levelCache;

		private static GeonBit.UI.Entities.Panel menu;



		public GameWorld() : base(new GameplayScreen())
		{
			self = this;

			random = new Random();

			Paused = false;

			input = new Input();          

			levelCache = new Dictionary<string, Level>();

			currentActiveControl = null;
			menu = null;

			PlayerEntity playerEntity = new PlayerEntity(new Vector2(0f))
			{
				input = input
			};
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


			input.Update(!(UserInterface.Active.ActiveEntity is GeonBit.UI.Entities.RootPanel));


			if (input.KeyCheckPressed(Microsoft.Xna.Framework.Input.Keys.Escape))
				Pause();


			if (!Paused)
			{
				mapRenderer.Update(delta);

				if (input.KeyCheckPressed(Core.IO.Control.Interact))
					CurrentActiveControl = null;

				player.Update(delta);

				level.Update(delta);

				if (PlayerExists)
				{
					Camera.LookAt(PlayerMob.Position);
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
		}


		public override void Draw(SpriteBatch spriteBatch)
		{
			base.Draw(spriteBatch);

			mapRenderer.Draw(Camera.GetViewMatrix());
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


		public static void DisolveToParticles(Texture2D texture, Vector2 position)
		{
			//Texture2D texture = entity.Sprite.GetTexture();

			Color[] data = new Color[texture.Width * texture.Height];

			texture.GetData(data);

			float maxParticleLife = Properties.Settings.Default.limitParticles ? 3 : 20;

			for(int i = 0; i < data.Length; i++)
			{
				if (data[i].A > 127)
				{
					EntityManager.CreateEntity(new Particle(position + new Vector2(i % texture.Width, i / texture.Height), "pixel", "None", data[i], new Angle((float)random.NextDouble(), AngleType.Revolution), random.Next(100, 350), random.NextSingle(1, maxParticleLife)));
				}
			}
		}

		public static void DisolveToParticles(Entity entity)
		{
			DisolveToParticles(entity.Sprite.GetTexture(), entity.Position - entity.Sprite.Sprite.origin.ToVector2());
		}


		public void Pause()
		{
			if (!Paused)
			{
				Paused = true;
				menu = (GeonBit.UI.Entities.Panel)UserInterface.Active.AddEntity(new PauseScreen());
			}
			else
			{
				Paused = false;
				UserInterface.Active.ActiveEntity = UserInterface.Active.Root;
				menu.RemoveFromParent();
			}
		}
	}
}
