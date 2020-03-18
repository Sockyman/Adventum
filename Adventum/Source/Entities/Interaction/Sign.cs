using System;
using Microsoft.Xna.Framework;
using Adventum.UI;
using GeonBit.UI.Entities;
using Microsoft.Xna.Framework.Graphics;
using Adventum.Core.Resource;

namespace Adventum.Entities.Interaction
{
    public class Sign : Entity, IInteractable
    {
		public const int alertDistance = 100;

		public virtual bool ShowAlert => true;

		public PanelSkin skin;

        public string title;
        public string text;

		public Sign(Vector2 position, string title, string text, int skin = 0) : base(position)
        {
            visible = false;

            Sprite = new Sprite.Animator("Furniture2", "chair");

			Solid = false;

            this.text = text;
            this.title = title;

			this.skin = (PanelSkin)skin;
        }


		public override void Draw(SpriteBatch spriteBatch)
		{
			base.Draw(spriteBatch);


			

			if (ShowAlert && Vector2.Distance(Position, Main.gameState.Camera.Center) < alertDistance)
			{
				Texture2D alert = ResourceManager.GetTexture("alert");
				Vector2 drawPosition = Position;
				drawPosition.Y -= alert.Height * 3;
				drawPosition.X -= alert.Width / 2;
				spriteBatch.Draw(alert, drawPosition, color: Color.White, layerDepth: 1f);
			}
		}


		public virtual void OnExamine()
        {
			Show();
        }

		public void Show()
		{
			Main.gameState.screen.AddChild(new TextBox(title, text, skin));
		}
    }
}
