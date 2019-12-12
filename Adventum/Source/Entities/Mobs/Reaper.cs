using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Adventum.World;
using Adventum.States;
using MonoGame.Extended;
using Adventum.Util;
using Adventum.Data;
using Adventum.Sprite;

namespace Adventum.Entities.Mobs
{
    class Reaper : Enemy
    {
        public Reaper(Vector2 position) : base(position, "reaper", maxHealth: 5)
        {
            
        }

        protected override void InitalizeBehavior()
        {
            base.InitalizeBehavior();

            state.AddState(EState.Idle).AddStateTrigger(EState.Walk, () => true);
        }
    }
}
