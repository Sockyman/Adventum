using System;
using Microsoft.Xna.Framework;
using Adventum.Data;
using Adventum.Source.States;
using MonoGame.Extended;

namespace Adventum.Source.Entities
{
    public class Mob : Entity
    {
        public Mob(Vector2 position) : base(position)
        {
            
        }


        protected override void InitalizeBehavior()
        {
            base.InitalizeBehavior();

            state.AddState(EState.Idle).AddStateTrigger(EState.Walk, () =>
                PreviousVelocity != Vector2.Zero
            ).AddEntranceTrigger(() => Sprite.TryChangeAnimation("idle"));

            state.AddState(EState.Walk).AddStateTrigger(EState.Idle, () =>
                PreviousVelocity == Vector2.Zero
            ).AddEntranceTrigger(() => Sprite.TryChangeAnimation("walk"));

            state.AddState(EState.Attack).AddCountdownStateTrigger(EState.Idle, 2f);
        }


        public override void Move(Vector2 angle, bool changeDirection = false)
        {
            base.Move(angle, changeDirection);
            if (changeDirection && angle != Vector2.Zero)
            {

            }
                
        }
    }
}
