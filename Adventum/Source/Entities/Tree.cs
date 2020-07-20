using System;
using Microsoft.Xna.Framework;
using Adventum.Core.Collision;
using Adventum.Sprite;
using Adventum.Data;
using Adventum.Core.Resource;

namespace Adventum.Entities
{
    public class Tree : Entity
    {
        public override CollisionType CollisionType => CollisionType.Immovable;
        public override bool ReactToCollisions => false;


        public override bool CheckCollisions { get; }

        public Tree(Vector2 position, bool collides) : base(position)
        {
            Sprite = new Animator("Tree", "tree");
            Sprite.FrameNumber += (float)random.NextDouble() * 4;

            CheckCollisions = collides;

            SetBounds(new Point(16));
        }
    }
}
