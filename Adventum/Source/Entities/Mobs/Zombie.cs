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
        public Zombie(Vector2 position) : base(position, "zombieBase", maxHealth: 2)
        {
        }


        protected override void InitalizeBehavior()
        {
            base.InitalizeBehavior();

            state.AddState(EState.Idle).AddCountdownStateTrigger(EState.Walk, 0f);

            state.AddState(EState.Walk).AddStateTrigger(EState.Idle, () => random.Next(100) > 93 && state.clock.CurrentTime.TotalSeconds > 0.5)
                .AddEntranceTrigger(() => state.Facing = (Direction)random.Next(8)).AddStateTrigger(EState.Attack, () =>
                {
                    return GameWorld.EntityExists(GameWorld.player.player) && Vector2.Distance(Position, GameWorld.player.player.Position) < 30 &&
                        Utils.AngleToDirection(Angle.FromVector(GameWorld.player.player.Position - Position)) == state.Facing;
                });

            state.AddState(EState.Attack).AddEntranceTrigger(() =>
            {
                
                //GameWorld.entityManager.CreateEntity(new Arrow(this, GameWorld.player.player.Position - Position, 2000));
            });
        }

        public override void UseMain()
        {
            base.UseMain();

            //GameWorld.entityManager.CreateEntity(new Attack(this, new Point(32), Utils.DirectionToVector(state.Facing), 0.075f, 500));
        }
    }
}
