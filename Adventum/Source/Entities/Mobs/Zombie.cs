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
    class Zombie : Enemy
    {
        public override float MaxMovementSpeed => base.MaxMovementSpeed / 2;

        public Zombie(Vector2 position) : base(position, "zombieBase", maxHealth: 2)
        {
        }


        protected override void InitalizeBehavior()
        {
            base.InitalizeBehavior();

            state.AddState("Idle").AddStateTrigger("Walk", () => true);

            state.AddState("Walk").AddStateTrigger("Idle", () => random.Next(100) > 93 && state.clock.CurrentTime.TotalSeconds > 0.5)
                .AddEntranceTrigger(() => state.Facing = (Direction)random.Next(8)).AddStateTrigger("Attack", () =>
                {
                    return GameWorld.PlayerExists && Vector2.Distance(Position, GameWorld.PlayerMob.Position) < 30 &&
                        Utils.AngleToDirection(Angle.FromVector(GameWorld.PlayerMob.Position - Position)) == state.Facing;
                }).AddUpdateTrigger(() =>
                {
                    if (GameWorld.PlayerExists && Vector2.Distance(Position, GameWorld.PlayerMob.Position) < 200)
                    {
                        state.Facing = Utils.AngleToDirection(Angle.FromVector(GameWorld.PlayerMob.Position - Position));
                    }
                });
        }

        public override void UseMain()
        {
            base.UseMain();

            //GameWorld.entityManager.CreateEntity(new Attack(this, new Point(32), Utils.DirectionToVector(state.Facing), 0.075f, 500));
        }
    }
}
