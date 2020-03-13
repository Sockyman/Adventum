using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Adventum.Util;
using GeonBit.UI;
using GeonBit.UI.Entities;

namespace Adventum.GameStates
{
	public abstract class GameState
	{
		public Panel screen;

		public GameState(Panel screen)
		{
			this.screen = screen;
			UserInterface.Active.AddEntity(screen);
		}

		public virtual void Update(DeltaTime delta) { }

		public virtual void Draw(SpriteBatch spriteBatch) { }
	}
}
