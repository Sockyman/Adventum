using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Adventum.World;
using Adventum.States;
using MonoGame.Extended;
using Adventum.Util;
using Adventum.Data;
using Adventum.Sprite;
using Adventum.Entities.Interaction;

namespace Adventum.Entities.Mobs
{
    class Reaper : Enemy
    {
        public override float MaxMovementSpeed => base.MaxMovementSpeed / 4;


        public Reaper(Vector2 position) : base(position, "reaper", maxHealth: 5)
        {
            
        }

        protected override void InitalizeBehavior()
        {
            base.InitalizeBehavior();

            state.AddState(EState.Idle).AddTrigger(() =>
                {
                    state.Facing = Utils.AngleToDirection(new Angle((float)random.NextDouble(), AngleType.Revolution), true);
                    state.SetActiveState(EState.Walk);
                }, () => state.clock.CurrentTime.TotalMilliseconds > random.Next(100, 3000))
                .AddUpdateTrigger(() =>
                {
                    if (GameWorld.PlayerExists && Vector2.Distance(Position, GameWorld.PlayerMob.Position) < 300)
                    {
                        state.Facing = Utils.AngleToDirection(Angle.FromVector(GameWorld.PlayerMob.Position - Position));
                        state.SetActiveState(EState.Charging);
                    }
                });

            state.AddState(EState.Charging).AddCountdownStateTrigger(EState.Attack, 0.5f).AddUpdateTrigger(() =>
            {
                Move(-Utils.DirectionToVector(state.Facing), 100);
            });

            state.AddState(EState.Attack).AddCountdownStateTrigger(EState.Walk, 0.23f);

            state.AddState(EState.Walk).AddStateTrigger(EState.Idle, () => state.clock.CurrentTime.TotalMilliseconds > random.Next(2000, 20000));
        }


        public override void UseMain()
        {
            GameWorld.entityManager.CreateEntity(new Arrow(this, Utils.DirectionToVector(state.Facing)));
        }
    }
}
