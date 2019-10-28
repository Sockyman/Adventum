using Microsoft.Xna.Framework;
using Adventum.Data;
using MonoGame.Extended;

namespace Adventum.Source.Entities
{
    public class Mob : Entity
    {
        public Mob(Vector2 position) : base(position)
        {

        }



        public override void Move(Vector2 angle, float speed, bool changeDirection = false)
        {
            base.Move(angle, speed, changeDirection);

            if (changeDirection)
            {
                state.Facing = (Direction)((Angle.FromVector(angle).Degrees * -1 + 180) / 90);
            }
        }
    }
}
