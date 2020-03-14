using System;
using System.Collections.Generic;
using GeonBit.UI;
using GeonBit.UI.Entities;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Adventum.World;
using Adventum.Util;
using Adventum.Core.Resource;

namespace Adventum.UI
{
	class TitleScreen : Panel
	{
		public TitleScreen() : base(new Vector2(Main.graphics.PreferredBackBufferWidth, Main.graphics.PreferredBackBufferHeight), PanelSkin.None)
		{
			void AddButton(string name, EventCallback onClick)
			{
				Button button = new SimpleButton(name, onClick);
				AddChild(button);
			}

			HorizontalLine line = new HorizontalLine(Anchor.AutoCenter, Utils.ScaleToScreen(new Vector2(0, 100))) { Opacity = 0 };
			AddChild(line);

			AddButton("Play", OnPlayButtonClick);
			AddButton("Exit", OnExitButtonClick);
		}

		private void OnPlayButtonClick(Entity entity)
		{
			Main.ChangeState<GameWorld>();
		}

		private void OnExitButtonClick(Entity entity)
		{
			Main.EndGame();
		}

		public override void Draw(SpriteBatch spriteBatch)
		{
			base.Draw(spriteBatch);

			//spriteBatch.Draw(gameTitle, DrawPosition, Color.White);
		}
	}
}
