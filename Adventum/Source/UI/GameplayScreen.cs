using System;
using System.Collections.Generic;
using GeonBit.UI;
using GeonBit.UI.Entities;
using Microsoft.Xna.Framework;
using Adventum.World;
using Adventum.Util;

namespace Adventum.UI
{
    public class GameplayScreen : Panel
    {
        public ProgressBar healthBar;

        public GameplayScreen() : base(new Vector2(Main.graphics.PreferredBackBufferWidth, Main.graphics.PreferredBackBufferHeight), PanelSkin.None)
        {
            Locked = true;
            Visible = true;

            healthBar = new ProgressBar(0, 100, Utils.ScaleToScreen(new Vector2(200, 14)), Anchor.BottomCenter);
            AddChild(healthBar);
            healthBar.Value = 0;

        }

        public override void Update(ref Entity targetEntity, ref Entity dragTargetEntity, ref bool wasEventHandled, Point scrollVal)
        {
            base.Update(ref targetEntity, ref dragTargetEntity, ref wasEventHandled, scrollVal);

            var player = GameWorld.player.player;

            if (GameWorld.EntityExists(player))
            {
                healthBar.Max = (uint)player.MaxHealth * 5000;

                healthBar.Value = player.Health * 5000;
            }
            else
            {
                healthBar.Value = 0;
            }
        }
    }
}
