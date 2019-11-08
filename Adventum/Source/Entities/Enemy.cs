using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Adventum.Source.States;
using Adventum.Source.Util;
using Adventum.Data;
using MonoGame.Extended;

namespace Adventum.Source.Entities
{
    class Enemy : Mob
    {
        public Enemy(Vector2 position) : base(position)
        {
        }

        protected override void InitalizeBehavior()
        {
            base.InitalizeBehavior();

            state.AddState(EState.Idle).AddCountdownStateTrigger(EState.Walk, 1);

            state.AddState(EState.Walk).AddCountdownStateTrigger(EState.Idle, 2).AddEntranceTrigger(() => state.Facing = (Direction)random.Next(4))
                .AddUpdateTrigger( () =>
                {
                    Move(Utils.DirectionToVector(state.Facing), MaxMovementSpeed, true);
                });
        }


        public override void Update(DeltaTime delta)
        {
            base.Update(delta);

            Console.WriteLine(state);
        }
    }
}
