using System;
using Microsoft.Xna.Framework;
using Adventum.Core.Collision;
using Adventum.Sprite;
using Adventum.Data;
using Adventum.Core.Resource;

namespace Adventum.Entities
{
    class Tree : Entity
    {
        public override CollisionType CollisionType => CollisionType.Immovable;
        public override bool ReactToCollisions => false;

        public Tree(Vector2 position) : base(position)
        {
            Sprite = new Animator("Tree", "tree");
            Sprite.FrameNumber += (float)random.NextDouble() * 4;

            SetBounds(new Point(16));
        }
    }
}
