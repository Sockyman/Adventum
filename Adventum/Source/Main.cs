using System;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using MonoGame.Extended;
using Adventum.Core;
using Adventum.Core.Resource;
using Adventum.World;
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
        public static Size windowSize = new Size(640, 360);
        public static GraphicsDeviceManager graphics;
        public static GameWorld gameWorld;
        public static RenderTarget2D renderTarget;
        public static StringBuilder debugString = new StringBuilder();

        public static UserInterface Ui;

        SpriteBatch spriteBatch;

        public static OrthographicCamera Camera { get; set; }



        public Main()
        {
            self = this;

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
            BoxingViewportAdapter viewPort = new BoxingViewportAdapter(Window, GraphicsDevice, windowSize.Width, windowSize.Height);
            Camera = new OrthographicCamera(viewPort);

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
            renderTarget = new RenderTarget2D(GraphicsDevice, windowSize.Width, windowSize.Height);


            ResourceManager.LoadContent(Content);


            UserInterface.Initialize(Content, BuiltinThemes.editor);
            UserInterface.Active.UseRenderTarget = true;
            UserInterface.Active.SamplerState = SamplerState.PointClamp;

            UserInterface.Active.GlobalScale = 1.75f;
            UserInterface.Active.ShowCursor = false;

            Panel panel = new Panel(new Vector2(100, 100), anchor: Anchor.TopLeft);
            UserInterface.Active.AddEntity(panel);
            Button b = new Button("Hi");
            panel.AddChild(b);


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


            UserInterface.Active.Update(gameTime);

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
            
            UserInterface.Active.RenderTargetTransformMatrix = Camera.GetViewMatrix();

            GraphicsDevice.SetRenderTarget(renderTarget);


            /// Drawing at world position (Game).
            {
                spriteBatch.Begin(SpriteSortMode.FrontToBack, samplerState: SamplerState.PointClamp, transformMatrix: Camera.GetViewMatrix());

                //ResourceManager.GetShader("fullWhite").CurrentTechnique.Passes[0].Apply();
                gameWorld.Draw(spriteBatch);

                spriteBatch.End();
            }

            /// Drawing at screen position (GUI).
            {
                spriteBatch.Begin(SpriteSortMode.FrontToBack, samplerState: SamplerState.PointClamp);

                spriteBatch.Draw(UserInterface.Active.RenderTarget, new Rectangle(0, 0, windowSize.Width, windowSize.Height), Color.White);

                DrawCursor(spriteBatch);

                spriteBatch.End();
            }

            

            GraphicsDevice.SetRenderTarget(null);
            /// Drawing render target to screen and debug.
            {
                spriteBatch.Begin(SpriteSortMode.Immediate, BlendState.AlphaBlend, SamplerState.PointClamp);

                ResourceManager.GetShader("deSaturate").CurrentTechnique.Passes[0].Apply();
                spriteBatch.Draw(renderTarget, new Rectangle(0, 0, graphics.PreferredBackBufferWidth, graphics.PreferredBackBufferHeight), Color.White);
                



                spriteBatch.DrawString(ResourceManager.GetFont("fontMain"), debugString, new Vector2(40, 40), Color.White);
                debugString = new StringBuilder("Adventum ");

                DebugAdd(System.Reflection.Assembly.GetEntryAssembly().GetName().Version.ToString());
                DebugAdd(":");
                DebugAdd(((int)GameWorld.deltaTime.FPS).ToString(), "FPS:");
                DebugAdd(Camera.Position.ToPoint().ToString(), "CameraPosition:");

                spriteBatch.End();
            }

            //UserInterface.Active.DrawMainRenderTarget(spriteBatch);

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


        public static Main DebugAdd(string s, string title = "")
        {
            debugString.Append(title).Append(" ").Append(s).Append("    ");
            return self;
        }
    }
}
