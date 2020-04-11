using System;
using System.Collections.Generic;
using GeonBit.UI;
using GeonBit.UI.Entities;
using Microsoft.Xna.Framework;
using Adventum.World;
using Adventum.Util;
using Microsoft.Xna.Framework.Graphics;

namespace Adventum.UI
{
	public class GameplayScreen : Screen
	{
		public const string goldString = "Gold: ";

		public ProgressBar healthBar;
		public GeonBit.UI.Entities.Paragraph goldCount;

		public GameplayScreen() : base()
		{
			Locked = true;
			Visible = true;


			goldCount = new Paragraph(goldString, Anchor.AutoCenter, scale: 1.5f);
			healthBar = new ProgressBar(0, 100, Utils.ScaleToScreen(new Vector2(200, 14)), Anchor.BottomCenter);

			AddChild(goldCount);
			AddChild(healthBar);
			healthBar.Value = 0;

		}

		public override void Update()
		{
			base.Update();

			var player = GameWorld.player.player;

			if (GameWorld.EntityExists(player))
			{
				healthBar.Max = (uint)player.MaxHealth * 5000;

				healthBar.Value = player.Health * 5000;

				goldCount.Text = goldString + player.Gold.ToString();
			}
			else
			{
				healthBar.Value = 0;
			}
		}
	}
}
