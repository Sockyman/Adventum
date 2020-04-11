using System;
using GeonBit.UI;
using GeonBit.UI.Entities;
using Microsoft.Xna.Framework;

namespace Adventum.UI
{
	public class PauseScreen : Screen
	{
		public PauseScreen() : base()
		{
			AddChild(new HorizontalLine()).Opacity = 0;
			AddHeader("Paused", false);

			AddButton("Unpause", OnUnpauseButtonClick);
			AddButton("Options", OnOptionsButtonClick);
			AddButton("Exit To Title", OnTitleButtonClick);
			AddButton("Quit Game", OnExitButtonClick);
		}

		private void OnUnpauseButtonClick(Entity entity)
		{
			World.GameWorld.self.Pause();
		}

		private void OnOptionsButtonClick(Entity entity)
		{
			AddChild(new OptionsScreen());
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
