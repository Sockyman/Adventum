using System;
using System.Collections.Generic;
using Adventum.UI;
using Adventum.GameStates;
using Adventum.Core.Resource;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Adventum.GameStates
{
	public class TitleState : GameState
	{
		private const int titleYMargin = 5;

		private readonly Texture2D gameTitle;

		private Vector2 DrawPosition
		{
			get
			{
				return new Vector2(Main.windowSize.X / 2 - gameTitle.Width / 2, Main.windowSize.Y / 2 - (gameTitle.Height * titleYMargin) / 2);
			}
		}


		public TitleState() : base(new TitleScreen())
		{
			gameTitle = ResourceManager.GetTexture("title");

			LightColor = Color.White;
		}

		public override void Draw(SpriteBatch spriteBatch)
		{
			base.Draw(spriteBatch);

			spriteBatch.Draw(gameTitle, DrawPosition, Color.White);
		}
	}
}
