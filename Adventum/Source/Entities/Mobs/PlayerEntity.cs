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


        public PlayerEntity(Vector2 position) : base(position, "humanBase", "HumanoidBase", maxHealth: 100)
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
            state.AddState(EState.Attack).AddEntranceTrigger(() => Sprite.TryChangeAnimation("walk")).AddEntranceTrigger(() =>
            {
                //World.GameWorld.entityManager.CreateEntity(new Attack(this, new Point(32), Utils.DirectionToVector(state.Facing), 0.1f, 500));
                World.GameWorld.entityManager.CreateEntity(new Arrow(this, input.MouseWorldPosition - Position, 2000));
            });
        }


        public override void Update(DeltaTime delta)
        {
            base.Update(delta);

            Vector2 movement = new Vector2();
            movement.X += input.CheckAxis(Keys.A, Keys.D);
            movement.Y += input.CheckAxis(Keys.W, Keys.S);

            Move(movement, MaxMovementSpeed, false);

            state.Facing = Utils.AngleToDirection(Angle.FromVector(input.MouseWorldPosition - Position));
        }

        public override void OnCollision(CollisionData collisionData)
        {
            base.OnCollision(collisionData);
        }
    }
}
