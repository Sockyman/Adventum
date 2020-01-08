using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Adventum.Data;

namespace Adventum.Entities.Decor
{
    class Furniture : Entity
    {
        public Furniture(Vector2 position, string texture, Direction facing = Direction.Down) : base(position)
        {
            Solid = true;
            Sprite = new Sprite.Animator("Furniture2", texture);
            state.Facing = facing;

            SetBounds(new Point(16));
        }
    }
}
