using System;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using MonoGame.Extended;
using Adventum.Core;
using Adventum.Util;
using Adventum.Core.Resource;
using Adventum.World;
using Adventum.GameStates;
using MonoGame.Extended.ViewportAdapters;
using GeonBit.UI;
using GeonBit.UI.Entities;

namespace Adventum
{
	/// <summary>
	/// This is the main type for your game.
	/// </summary>
	public class Main : Game
	{
		private static Main self;
		public static Point windowSize = new Point(640, 360);
		public static GraphicsDeviceManager graphics;
		public static GameState gameState;

		public static RenderTarget2D renderTarget;
		public static RenderTarget2D lightsTarget;
		public static RenderTarget2D uiTarget;
		public static RenderTarget2D gameTarget;

		public static StringBuilder debugString = new StringBuilder();


		SpriteBatch spriteBatch;

		public static BoxingViewportAdapter viewPort;



		public Main()
		{
			self = this;

			graphics = new GraphicsDeviceManager(this);
			Content.RootDirectory = "Content";

			graphics.PreferredBackBufferWidth = GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Width;
			graphics.PreferredBackBufferHeight = GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Height;

			graphics.IsFullScreen = Properties.Settings.Default.startFullscreen;
			graphics.SynchronizeWithVerticalRetrace = false;

			IsFixedTimeStep = false;
		}



		/// <summary>
		/// Allows the game to perform any initialization it needs to before starting to run.
		/// This is where it can query for any required services and load any non-graphic
		/// related content.  Calling base.Initialize will enumerate through any components
		/// and initialize them as well.
		/// </summary>
		protected override void Initialize()
		{
			viewPort = new BoxingViewportAdapter(Window, GraphicsDevice, windowSize.X, windowSize.Y);

			base.Initialize();

		}



		/// <summary>
		/// LoadContent will be called once per game and is the place to load
		/// all of your content.
		/// </summary>
		protected override void LoadContent()
		{
			// Create a new SpriteBatch, which can be used to draw textures.
			spriteBatch = new SpriteBatch(GraphicsDevice);
			renderTarget = new RenderTarget2D(GraphicsDevice, windowSize.X, windowSize.Y);
			lightsTarget = new RenderTarget2D(GraphicsDevice, windowSize.X, windowSize.Y);
			uiTarget = new RenderTarget2D(GraphicsDevice, windowSize.X, windowSize.Y);
			gameTarget = new RenderTarget2D(GraphicsDevice, windowSize.X, windowSize.Y);


			ResourceManager.LoadContent(Content);


			UserInterface.Initialize(Content, "a");
			UserInterface.Active.UseRenderTarget = true;
			UserInterface.Active.SamplerState = SamplerState.PointClamp;

			UserInterface.Active.GlobalScale = 1f;
			UserInterface.Active.ShowCursor = false;


			ChangeState<TitleState>();

			Microsoft.Xna.Framework.Audio.SoundEffect.MasterVolume = Properties.Settings.Default.volume;
		}



		/// <summary>
		/// UnloadContent will be called once per game and is the place to unload
		/// game-specific content.
		/// </summary>
		protected override void UnloadContent()
		{
			// TODO: Unload any non ContentManager content here
		}



		/// <summary>
		/// Allows the game to run logic such as updating the world,
		/// checking for collisions, gathering input, and playing audio.
		/// </summary>
		/// <param name="gameTime">Provides a snapshot of timing values.</param>
		protected override void Update(GameTime gameTime)
		{
			if (Keyboard.GetState().IsKeyDown(Keys.OemTilde))
				End();

			if (Keyboard.GetState().IsKeyDown(Keys.F11))
			{
				graphics.IsFullScreen = true;
				graphics.ApplyChanges();
			}
			if (Keyboard.GetState().IsKeyDown(Keys.F10))
			{
				graphics.IsFullScreen = false;
				graphics.ApplyChanges();
			}


			DeltaTime delta = new DeltaTime(gameTime.TotalGameTime, gameTime.ElapsedGameTime);
			if (Properties.Settings.Default.showFps)
				DebugAdd(((int)delta.FPS).ToString(), "FPS:");
			gameState.Update(delta);


			UserInterface.Active.Update(gameTime);
			//DebugAdd(UserInterface.Active.ActiveEntity.ToString(), "ActiveControl:");


			base.Update(gameTime);
		}



		/// <summary>
		/// This is called when the game should draw itself.
		/// </summary>
		/// <param name="gameTime">Provides a snapshot of timing values.</param>
		protected override void Draw(GameTime gameTime)
		{
			GraphicsDevice.Clear(Color.Black);
			UserInterface.Active.Draw(spriteBatch);


			/// Drawing at world position (Game).
			GraphicsDevice.SetRenderTarget(renderTarget);
			{
				spriteBatch.Begin(SpriteSortMode.FrontToBack, samplerState: SamplerState.PointClamp, transformMatrix: gameState.Camera.GetViewMatrix());

				gameState.Draw(spriteBatch);

				spriteBatch.End();
			}


			/// Drawing Lighting effects
			if (Properties.Settings.Default.lightingEffects)
			{
				GraphicsDevice.SetRenderTarget(lightsTarget);

				GraphicsDevice.Clear(gameState.LightColor);

				spriteBatch.Begin(SpriteSortMode.Immediate, BlendState.Additive, samplerState: SamplerState.PointClamp, transformMatrix: gameState.Camera.GetViewMatrix());
				var lm = ResourceManager.GetTexture("lightMask");
				if (gameState is GameWorld gW)
				{
					gW.DrawLight(spriteBatch, lm);
				}

				spriteBatch.End();
			}


			/// Drawing at screen position (GUI).
			GraphicsDevice.SetRenderTarget(uiTarget);
			{
				GraphicsDevice.Clear(Color.Transparent);

				spriteBatch.Begin(SpriteSortMode.FrontToBack, samplerState: SamplerState.PointClamp);
				

				DrawCursor(spriteBatch);

				spriteBatch.End();
			}



			GraphicsDevice.SetRenderTarget(gameTarget);
			{
				spriteBatch.Begin(SpriteSortMode.Immediate, BlendState.AlphaBlend, SamplerState.PointClamp);

				if (Properties.Settings.Default.desaturate)
					ResourceManager.GetShader("deSaturate").CurrentTechnique.Passes[0].Apply();

				if (Properties.Settings.Default.lightingEffects)
				{
					ResourceManager.GetShader("lightEffect").Parameters["lightMask"].SetValue(lightsTarget);
					ResourceManager.GetShader("lightEffect").CurrentTechnique.Passes[0].Apply();
				}

				spriteBatch.Draw(renderTarget, new Rectangle(0, 0, windowSize.X, windowSize.Y), Color.White);
				//spriteBatch.Draw(lightsTarget, new Rectangle(0, 0, windowSize.X, windowSize.Y), Color.White);

				spriteBatch.End();
			}


			/// Drawing render targets to screen and debug.
			GraphicsDevice.SetRenderTarget(null);
			{
				

				spriteBatch.Begin(SpriteSortMode.Immediate, BlendState.AlphaBlend, SamplerState.PointClamp);

				spriteBatch.Draw(gameTarget, new Rectangle(0, 0, graphics.PreferredBackBufferWidth, graphics.PreferredBackBufferHeight), Color.White);

				if (Properties.Settings.Default.desaturate)
					ResourceManager.GetShader("deSaturate").CurrentTechnique.Passes[0].Apply();

				spriteBatch.Draw(UserInterface.Active.RenderTarget, new Rectangle(0, 0, UserInterface.Active.ScreenWidth, UserInterface.Active.ScreenHeight), Color.White);
				spriteBatch.Draw(uiTarget, new Rectangle(0, 0, graphics.PreferredBackBufferWidth, graphics.PreferredBackBufferHeight), Color.White);


				spriteBatch.DrawString(ResourceManager.GetFont("fontMain"), debugString, new Vector2(10, 10), Color.White);
				debugString = new StringBuilder("Adventum ");

				DebugAdd(System.Reflection.Assembly.GetEntryAssembly().GetName().Version.ToString());    


				spriteBatch.End();
			}


			base.Draw(gameTime);
		}


		public static void ChangeState<T>() where T : GameState, new()
		{
			UserInterface.Active.Clear();
			UserInterface.Active.ActiveEntity = UserInterface.Active.Root;
			gameState = Activator.CreateInstance<T>();
		}


		public static void EndGame()
		{
			self.End();
		}
		public void End()
		{
			Exit();
		}


		public void DrawCursor(SpriteBatch spriteBatch)
		{
			Texture2D cursorTexture = ResourceManager.GetTexture("cursor");
			Vector2 mousePosition = Mouse.GetState().Position.ToVector2();
			mousePosition.X = mousePosition.X / graphics.PreferredBackBufferWidth * renderTarget.Width - cursorTexture.Width / 2;
			mousePosition.Y = mousePosition.Y / graphics.PreferredBackBufferHeight * renderTarget.Height - cursorTexture.Height / 2;
			spriteBatch.Draw(cursorTexture, mousePosition, Color.White);
		}


		public static Main DebugAdd(string s, string title = "")
		{
			debugString.Append(title).Append(" ").Append(s).Append("    ");
			return self;
		}
	}
}
