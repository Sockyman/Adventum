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
using Microsoft.Xna.Framework.Graphics;

namespace Adventum.Entities.Mobs
{
    class Reaper : Enemy
    {
        public const float reverseTime = 0.4f;

        public override string Loot => "Reaper";

        public override float MaxMovementSpeed => base.MaxMovementSpeed / 4;

        public Vector2 pointing = new Vector2();


        public Reaper(Vector2 position) : base(position, "reaper", maxHealth: 5)
        {
            
        }

        protected override void InitalizeBehavior()
        {
            base.InitalizeBehavior();

            state.AddState("Idle").AddTrigger(() =>
                {
                    state.Facing = Utils.AngleToDirection(new Angle((float)random.NextDouble(), AngleType.Revolution), true);
                    state.SetActiveState("Walk");
                }, () => state.clock.CurrentTime.TotalMilliseconds > random.Next(100, 3000))
                .AddUpdateTrigger(() =>
                {
                    if (GameWorld.PlayerExists && Vector2.Distance(Position, GameWorld.PlayerMob.Position) < 300)
                    {
                        state.Facing = Utils.AngleToDirection(Angle.FromVector(GameWorld.PlayerMob.Position - Position));
                        state.SetActiveState("Charging");
                    }
                });

            state.AddState("Charging").AddCountdownStateTrigger("Attack", reverseTime).AddUpdateTrigger(() =>
            {
                Move(-Utils.DirectionToVector(state.Facing), 100);
            }).AddEntranceTrigger(() => pointing = GameWorld.PlayerMob.Position);

            state.AddState("Attack").AddCountdownStateTrigger("Walk", 0.23f);

            state.AddState("Walk").AddStateTrigger("Idle", () => state.clock.CurrentTime.TotalMilliseconds > random.Next(2000, 5000));
        }


        public override void UseMain()
        {
            GameWorld.EntityManager.CreateEntity(new Arrow(this, pointing - Position));
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            base.Draw(spriteBatch);

            //spriteBatch.Draw(Core.Resource.ResourceManager.GetTexture("pixel"), pointing, Color.Red);
        }
    }
}
