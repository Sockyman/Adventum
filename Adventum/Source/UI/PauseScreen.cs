using System;
using GeonBit.UI;
using GeonBit.UI.Entities;
using Microsoft.Xna.Framework;

namespace Adventum.UI
{
	public class PauseScreen : Panel
	{
		public PauseScreen() : base(new Vector2(Main.graphics.PreferredBackBufferWidth, Main.graphics.PreferredBackBufferHeight), PanelSkin.None)
		{
			// TODO: Create a base screen class.
			Button AddButton(string name, EventCallback onClick)
			{
				Button button = new SimpleButton(name, onClick);
				AddChild(button);
				return button;
			}

			Locked = false;

			AddChild(new Header("Paused"));
			AddChild(new HorizontalLine()).Opacity = 0;

			AddButton("Unpause", OnUnpauseButtonClick);
			AddButton("Exit To Title", OnTitleButtonClick);
			AddButton("Quit Game", OnExitButtonClick);
		}

		private void OnUnpauseButtonClick(Entity entity)
		{
			RemoveFromParent();

			//World.GameWorld.self.Pause();
			// TODO: Remove this once it is otherwise working.
			World.GameWorld.self.Pause();
		}

		private void OnTitleButtonClick(Entity entity)
		{
			Main.ChangeState<GameStates.TitleState>();
		}

		private void OnExitButtonClick(Entity entity)
		{
			Main.EndGame();
		}

	}
}
