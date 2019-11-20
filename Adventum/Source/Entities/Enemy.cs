using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Adventum.Source.States;
using Adventum.Source.Util;
using Adventum.Data;
using MonoGame.Extended;

namespace Adventum.Source.Entities
{
    class Enemy : Mob
    {
        public Enemy(Vector2 position) : base(position)
        {
            Sprite.SpriteTexture = Core.Resource.ResourceManager.GetTexture("humanBase");
        }


        protected override void InitalizeBehavior()
        {
            base.InitalizeBehavior();

            state.AddState(EState.Idle).AddCountdownStateTrigger(EState.Walk, 0f);

            state.AddState(EState.Walk).AddStateTrigger(EState.Idle, () => random.Next(100) > 93 && state.clock.CurrentTime.TotalSeconds > 0.5)
                .AddEntranceTrigger(() => state.Facing = (Direction)random.Next(4))
                .AddUpdateTrigger( () =>
                {
                    Move(Utils.DirectionToVector(state.Facing), MaxMovementSpeed / 2, true);
                });
        }


        public override void Update(DeltaTime delta)
        {
            base.Update(delta);

            
            Console.WriteLine(state);
        }
    }
}
