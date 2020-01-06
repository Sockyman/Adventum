using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Adventum.Data;
using Adventum.States;
using Adventum.Sprite;
using MonoGame.Extended;
using Adventum.Core;
using Adventum.Core.Collision;
using Adventum.Util;
using Adventum.World;
using Adventum.Entities.Interaction;
using Adventum.Core.Resource;

namespace Adventum.Entities.Mobs
{
    public class Mob : Entity
    {
        public int MaxHealth { get; private set; } = 2;
        public int Health { get; set; }
        public float HitFrames { get; private set; }

        protected float maxHitFrames = 0.1f;


        public Mob(Vector2 position) : base(position)
        {
            Sprite = new Animator("HumanoidBase", "humanBase");
            SetBounds(new Point(16));

            Health = MaxHealth;
        }

        public Mob(Vector2 position, string texture, string spriteSheet, int boundingSize = 16, int maxHealth = 10) : base(position)
        {
            Sprite = new Animator(spriteSheet, texture);
            SetBounds(new Point(boundingSize));

            MaxHealth = maxHealth;
            Health = MaxHealth;
        }


        protected override void InitalizeBehavior()
        {
            base.InitalizeBehavior();

            state.AddState(EState.Idle).AddEntranceTrigger(() => Sprite.TryChangeAnimation("idle"));

            state.AddState(EState.Walk).AddEntranceTrigger(() => Sprite.TryChangeAnimation("walk"));

            state.AddState(EState.Attack).AddCountdownStateTrigger(EState.Idle, 0.25f).AddEntranceTrigger(() => UseMain());
        }


        public override void Update(DeltaTime delta)
        {
            base.Update(delta);
            if (Health < 1)
            {
                Die();
            }

            HitFrames -= delta.Seconds;
        }


        public override void Draw(SpriteBatch spriteBatch)
        {
            if (HitFrames < 0 || random.Next(2) != 0)
            {
                //Sprite.Draw(spriteBatch, Position + new Vector2(-2, -2), new Color(Color.Black, 100));
                base.Draw(spriteBatch);
            }

            if (Health < MaxHealth)
            {
                Vector2 barCenter = Position;
                barCenter.Y -= Sprite.Sprite.frameSize.Y;
                Utils.DrawHealthBar(spriteBatch, barCenter, new Point(10, 2), MaxHealth, Health, Color.Red, Color.LimeGreen, 1f);
            }
        }


        public override void Move(Vector2 angle, bool changeDirection = false)
        {
            base.Move(angle, changeDirection);
        }

        public override void OnCollision(CollisionData collisionData)
        {
            base.OnCollision(collisionData);
        }


        public void Hurt(int damage, Angle direction)
        {
            if (HitFrames <= 0)
            {
                Health -= damage;

                HitFrames = maxHitFrames;

                direction.Revolutions += random.Next(-1, 1) / 10;
                ApplyDirecionalVelocity(direction, 500);

                Audio.Play("enemyHit0", 0.4f);
            }
        }


        public override void Die()
        {
            Audio.Play("enemyDeath0", 0.4f);

            base.Die();
        }


        public virtual void UseMain()
        {
            GameWorld.entityManager.CreateEntity(new Attack(this, new Point(32), Utils.DirectionToVector(state.Facing), 0.1f, 500, true, "attackSwish", 1));
        }
    }
}
