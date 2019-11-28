﻿using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Adventum.Core;
using Adventum.Core.Resource;
using Adventum.World;

namespace Adventum
{
    /// <summary>
    /// This is the main type for your game.
    /// </summary>
    public class Main : Game
    {
        public static GraphicsDeviceManager graphics;
        public static GameWorld gameWorld;
        public static RenderTarget2D renderTarget;
        SpriteBatch spriteBatch;



        public Main()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";

            graphics.PreferredBackBufferWidth = GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Width;
            graphics.PreferredBackBufferHeight = GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Height;

            graphics.IsFullScreen = false;
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
            // TODO: Add your initialization logic here

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
            renderTarget = new RenderTarget2D(GraphicsDevice, 640, 360);


            ResourceManager.LoadContent(Content);

            gameWorld = new GameWorld();
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
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();


            gameWorld.Update(gameTime);

            base.Update(gameTime);
        }



        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.Black);
            GraphicsDevice.SetRenderTarget(renderTarget);
            spriteBatch.Begin(SpriteSortMode.FrontToBack, samplerState: SamplerState.PointClamp);

            gameWorld.Draw(spriteBatch);


            DrawCursor(spriteBatch);

            spriteBatch.End();

            GraphicsDevice.SetRenderTarget(null);
            spriteBatch.Begin(SpriteSortMode.Immediate, BlendState.AlphaBlend, SamplerState.PointClamp, null, null);

            //ResourceManager.GetShader("fullWhite").CurrentTechnique.Passes[0].Apply();
            spriteBatch.Draw(renderTarget, new Rectangle(0, 0, graphics.PreferredBackBufferWidth, graphics.PreferredBackBufferHeight), Color.White);

            

            spriteBatch.DrawString(ResourceManager.GetFont("fontMain"), GameWorld.deltaTime.FPS.ToString(), new Vector2(40, 40), Color.White);
            spriteBatch.End();


            base.Draw(gameTime);
        }


        public void DrawCursor(SpriteBatch spriteBatch)
        {
            Texture2D cursorTexture = ResourceManager.GetTexture("cursor");
            Vector2 mousePosition = Mouse.GetState().Position.ToVector2();
            mousePosition.X = mousePosition.X / graphics.PreferredBackBufferWidth * renderTarget.Width - cursorTexture.Width / 2;
            mousePosition.Y = mousePosition.Y / graphics.PreferredBackBufferHeight * renderTarget.Height - cursorTexture.Height / 2;
            spriteBatch.Draw(ResourceManager.GetTexture("cursor"), mousePosition, Color.White);
        }
    }
}
