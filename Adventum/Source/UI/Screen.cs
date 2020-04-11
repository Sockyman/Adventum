using Microsoft.Xna.Framework;
using GeonBit.UI;
using GeonBit.UI.Entities;

namespace Adventum.UI
{
	public abstract class Screen : Panel
	{
		public Screen(PanelSkin skin = PanelSkin.None) : base(new Vector2(Main.graphics.PreferredBackBufferWidth, Main.graphics.PreferredBackBufferHeight), skin)
		{

		}


		protected Button AddButton(string name, EventCallback onClick)
		{
			Button button = new SimpleButton(name, onClick);
			AddChild(button);
			return button;
		}

		protected Header AddHeader(string name, bool dividerVisible = true)
		{
			Header head = new Header(name, Anchor.AutoCenter);
			HorizontalLine line = new HorizontalLine(Anchor.AutoCenter);
			line.Opacity = dividerVisible ? (byte)255 : (byte)0;
			AddChild(head);
			return head;
		}

		public virtual void Update()
		{

		}

		public override void Update(ref Entity targetEntity, ref Entity dragTargetEntity, ref bool wasEventHandled, Point scrollVal)
		{
			base.Update(ref targetEntity, ref dragTargetEntity, ref wasEventHandled, scrollVal);
			Update();
		}
	}
}
