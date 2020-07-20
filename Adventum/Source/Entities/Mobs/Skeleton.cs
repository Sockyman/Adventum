using Microsoft.Xna.Framework;
using Adventum.World;
using Adventum.Util;
using Adventum.Data;
using MonoGame.Extended;

namespace Adventum.Entities.Mobs
{
	public class Skeleton : Enemy
	{
        public override float MaxMovementSpeed => base.MaxMovementSpeed * 0.8f;

        public override string Loot => "Reaper";


        public Skeleton(Vector2 position) : base(position, "skeletonBase", maxHealth: 5)
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
	}
}
