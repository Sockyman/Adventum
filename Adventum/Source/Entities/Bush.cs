using Microsoft.Xna.Framework;

namespace Adventum.Entities
{
	public class Bush : Entity
	{
		public override bool CheckCollisions => false;


		public Bush(Vector2 position) : base(position)
		{
			Sprite = new Sprite.Animator("Bush", "bush");
		}
	}
}
