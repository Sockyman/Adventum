using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Adventum.Data;
using Adventum.States;
using Adventum.Core.Collision;
using Adventum.Entities.Interaction;

namespace Adventum.Entities.Mobs
{
    public class Slime : Enemy
    {
		public override float MaxMovementSpeed => base.MaxMovementSpeed / 2;


		public Slime(Vector2 position) : base(position, "slime", "Slime", 32, 3)
        {
			
        }


        protected override void InitalizeBehavior()
        {
            base.InitalizeBehavior();

            
            state.AddState(EState.Idle).AddCountdownStateTrigger(EState.Walk, 0.5f);

			state.AddState(EState.Walk).AddStateTrigger(EState.Idle, () =>  Sprite != null ? state.clock.CurrentTime.TotalSeconds > Sprite.Sprite.animations["walk"].frames * (1 / (float)Sprite.Sprite.animations["walk"].FPS) : false)
				.AddEntranceTrigger(() =>
				{
					if (World.GameWorld.PlayerExists && Vector2.Distance(Position, World.GameWorld.PlayerMob.Position) < 100)
						state.Facing = Util.Utils.VectorToDirection(World.GameWorld.PlayerMob.Position - Position);
					else
						state.Facing = (Direction)random.Next(0, 8);
				});
        }



		public override void OnCollision(CollisionData collisionData)
		{
			base.OnCollision(collisionData);

			if (collisionData.Other == World.GameWorld.PlayerMob && state.ActiveState == EState.Idle)
			{
				state.SetActiveState(EState.Attack);
			}
		}


		public override void UseMain()
		{
			MeleeAttack attack = new MeleeAttack(this, state.Facing, 1);
			attack.visible = false;

			World.GameWorld.entityManager.CreateEntity(attack);
		}
	}
}
