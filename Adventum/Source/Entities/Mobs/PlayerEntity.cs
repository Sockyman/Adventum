using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using MonoGame.Extended;
using Adventum.Core.IO;
using Adventum.Util;
using Adventum.States;
using Adventum.Core.Collision;
using Adventum.Data;

namespace Adventum.Entities.Mobs
{
    public class PlayerEntity : Mob
    {
        public Input input;


        public PlayerEntity(Vector2 position) : base(position, "humanBase", "HumanoidBase", maxHealth: 5)
        {
            input = new Input();
        }


        protected override void InitalizeBehavior()
        {
            base.InitalizeBehavior();

            state.AddState(EState.Idle).AddStateTrigger(EState.Attack, () => input.KeyCheck(MouseButton.Left)).AddStateTrigger(EState.Walk, () =>
                PreviousVelocity != Vector2.Zero
            );

            Trigger<EState> t = state.AddState(EState.Idle).RecentTrigger();

            state.AddState(EState.Walk).AttachTrigger(t).AddStateTrigger(EState.Idle, () =>
                PreviousVelocity == Vector2.Zero
            ).AddStateTrigger(EState.Attack, () => input.KeyCheck(MouseButton.Left));

            state.AddState(EState.Attack).AddEntranceTrigger(() => Sprite.TryChangeAnimation("walk"));
        }


        public override void Update(DeltaTime delta)
        {
            base.Update(delta);

            Vector2 movement = new Vector2();
            movement.X += input.CheckAxis(Keys.A, Keys.D);
            movement.Y += input.CheckAxis(Keys.W, Keys.S);

            Move(movement, MaxMovementSpeed, false);

            Vector2 centerPosition = Position;
            centerPosition.Y -= Sprite.Sprite.frameSize.Y / 2;
            state.Facing = Utils.AngleToDirection(Angle.FromVector(input.MouseWorldPosition - centerPosition));


            Main.DebugAdd(state.Facing.ToString(), "PlayerFacing:");
        }

        public override void OnCollision(CollisionData collisionData)
        {
            base.OnCollision(collisionData);
        }


        public override void UseMain()
        {
            base.UseMain();
        }
    }
}
