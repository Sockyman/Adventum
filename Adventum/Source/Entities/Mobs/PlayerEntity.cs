using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using MonoGame.Extended;
using Adventum.Core.IO;
using Adventum.Util;
using Adventum.States;
using Adventum.Core.Collision;
using Adventum.World;
using Adventum.Data;

namespace Adventum.Entities.Mobs
{
    public class PlayerEntity : Mob, ILightEmiter
    {
        public Input input;

        public int LightRadius => 75;
        public Color LightColor => Color.White;

        public PlayerEntity(Vector2 position) : base(position, "playerBase", "HumanoidBase", maxHealth: 10)
        {
            input = new Input();

			alignment = Alignment.Good;
        }


        protected override void InitalizeBehavior()
        {
            base.InitalizeBehavior();

            state.AddState(EState.Idle).AddStateTrigger(EState.Attack, () => input.KeyCheck(MouseButton.Left)).AddStateTrigger(EState.Walk, () =>
                PreviousVelocity != Vector2.Zero
            ).AddStateTrigger(EState.Interact, () => input.KeyCheck(MouseButton.Right));

            Trigger<EState> t = state.AddState(EState.Idle).RecentTrigger();

            state.AddState(EState.Walk).AttachTrigger(t).AddStateTrigger(EState.Idle, () =>
                PreviousVelocity == Vector2.Zero
            ).AddStateTrigger(EState.Attack, () => input.KeyCheck(MouseButton.Left)).AddStateTrigger(EState.Interact, () => input.KeyCheck(MouseButton.Right));

            

        }


        public override void Update(DeltaTime delta)
        {
            base.Update(delta);

			maxHitFrames = 0.6f;

            Vector2 movement = new Vector2();
            movement.X += input.CheckAxis(Keys.A, Keys.D);
            movement.Y += input.CheckAxis(Keys.W, Keys.S);

            Move(movement, MaxMovementSpeed, false);

            Vector2 centerPosition = Position;
            centerPosition.Y -= Sprite.Sprite.frameSize.Y / 2;
            state.Facing = Utils.AngleToDirection(Angle.FromVector(input.MouseWorldPosition - centerPosition));
        }

        public override void OnCollision(CollisionData collisionData)
        {
            base.OnCollision(collisionData);

			if (collisionData.Other is Door)
			{
                Door door = (Door)collisionData.Other;

                Core.Audio.Play("LevelChange");
				GameWorld.LoadLevel(door.level, door.cacheLevel, this);
			}
        }


        public override void UseMain()
        {
            base.UseMain();
        }


        public override void UseSecondary()
        {
            base.UseSecondary();

            World.GameWorld.EntityManager.CreateEntity(new Interaction.Examine(this, Utils.DirectionToVector(state.Facing)));
        }
    }
}
