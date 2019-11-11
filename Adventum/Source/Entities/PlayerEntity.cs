using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using Adventum.Source.Core.IO;
using Adventum.Source.Util;
using Adventum.Source.States;
using Adventum.Data;

namespace Adventum.Source.Entities
{
    public class PlayerEntity : Mob
    {
        public Input input;


        public PlayerEntity(Vector2 position) : base(position)
        {
            input = new Input();
        }


        protected override void InitalizeBehavior()
        {
            base.InitalizeBehavior();

            state.AddState(EState.Idle).AddStateTrigger(EState.Attack, () => input.KeyCheckPressed(Keys.Space)).AddStateTrigger(EState.Walk, () =>
                PreviousVelocity != Vector2.Zero
            );
            Trigger<EState> t = state.AddState(EState.Idle).RecentTrigger();
            state.AddState(EState.Walk).AttachTrigger(t).AddStateTrigger(EState.Idle, () =>
                PreviousVelocity == Vector2.Zero
            );
            state.AddState(EState.Attack).AddEntranceTrigger(() => Sprite.TryChangeAnimation("walk"));
        }


        public override void Update(DeltaTime delta)
        {
            base.Update(delta);

            Vector2 movement = new Vector2();
            movement.X += input.CheckAxis(Keys.A, Keys.D);
            movement.Y += input.CheckAxis(Keys.W, Keys.S);

            Move(movement, MaxMovementSpeed, true);

            /*
            if (state.ActiveState == EState.Walk)
            {
                Sprite.TryChangeAnimation("walk");
            }
            else
            {
                Sprite.TryChangeAnimation("idle");
            }*/

            Console.WriteLine(state.Facing);
        }
    }
}
