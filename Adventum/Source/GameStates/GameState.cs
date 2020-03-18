using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Adventum.Util;
using GeonBit.UI;
using GeonBit.UI.Entities;
using MonoGame.Extended;
using MonoGame.Extended.ViewportAdapters;

namespace Adventum.GameStates
{
	public abstract class GameState
	{
		public Panel screen;
		public OrthographicCamera Camera { get; protected set; }

		public Color LightColor { get; set; }

		public GameState(Panel screen)
		{
			this.screen = screen;
			Camera = new OrthographicCamera(Main.viewPort);
			UserInterface.Active.AddEntity(screen);
		}

		public virtual void Update(DeltaTime delta) { }

		public virtual void Draw(SpriteBatch spriteBatch) 
		{
			//spriteBatch.Draw(Core.Resource.ResourceManager.GetTexture("pixel"), new Rectangle(0, 0, 100, 100), Color.Red);
		}
	}
}
