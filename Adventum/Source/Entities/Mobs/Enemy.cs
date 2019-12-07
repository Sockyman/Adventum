using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Adventum.States;
using Adventum.Util;
using Adventum.Sprite;
using Adventum.Data;
using Adventum.World;
using MonoGame.Extended;

namespace Adventum.Entities.Mobs
{
    class Enemy : Mob
    {
        public Enemy(Vector2 position) : base(position)
        {
            
            Sprite = new Animator("HumanoidBase", "zombieBase");
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
                })
                .AddUpdateTrigger( () =>
                {
                    Move(Utils.DirectionToVector(state.Facing), MaxMovementSpeed / 2, true);
                });

            state.AddState(EState.Attack).AddEntranceTrigger(() =>
            {
                GameWorld.entityManager.CreateEntity(new Attack(this, new Point(32), Utils.DirectionToVector(state.Facing), 0.075f, 500));
                //GameWorld.entityManager.CreateEntity(new Arrow(this, GameWorld.player.player.Position - Position, 2000));
            });
        }


        public override void Update(DeltaTime delta)
        {
            base.Update(delta);
        }
    }
}
