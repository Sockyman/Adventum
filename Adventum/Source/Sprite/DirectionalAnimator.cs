using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Adventum.Data;
using Adventum.Source.Util;
using Adventum.Source.States;

namespace Adventum.Source.Sprite
{
    public class DirectionalAnimator : Animator
    {
        public Direction Facing { get; private set; }

        public DirectionalAnimator(string spriteSheet, Texture2D texture) : base(spriteSheet, texture)
        {
            Facing = Direction.Down;
        }


        public void UpdateState(DeltaTime delta, EntityState state)
        {
            Update(delta);

            Facing = state.Facing;

            TryChangeAnimation(state.State);
        }


        public override Texture2D GetTexture()
        {
            return base.GetTexture();
        }
    }
}
