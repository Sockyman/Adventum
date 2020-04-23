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
        public int Health
        {
            get
            {
                return health;
            }
            set
            {
                health = Math.Min(MaxHealth, value);
            }
        }
        private int health;

        public int Gold { get; set; } = 0;

        public float HitFrames { get; private set; }


        public virtual string Loot => "";

        public virtual string[] HitSound => new string[] { "enemyHit0", "enemyHit1" };
        public virtual string[] DeathSound => new string[] { "enemyDeath0" };
        public virtual string[] AmbientSound => new string[] { };
        public virtual float AmbientChance => 0.035f;


        public virtual bool ShowHealthbar => false;


        protected float maxHitFrames = 0.2f;

		public Alignment alignment = Alignment.Neutral;


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

            state.AddState("Idle").AddEntranceTrigger(() => Sprite.TryChangeAnimation("idle"));

            state.AddState("Walk").AddEntranceTrigger(() => Sprite.TryChangeAnimation("walk"));
            state.AddState("Attack").AddEntranceTrigger(() => Sprite.TryChangeAnimation("attack"))
                .AddCountdownStateTrigger("Idle", 0.25f).AddEntranceTrigger(() => UseMain());

            state.AddState("Interact").AddCountdownStateTrigger("Idle", 0.25f).AddEntranceTrigger(() => UseSecondary()).AddEntranceTrigger(() => Sprite.TryChangeAnimation("walk"));
        }


        public override void Update(DeltaTime delta)
        {
            base.Update(delta);
            if (Health < 1)
            {
                Die();
            }


            if (random.NextDouble() < AmbientChance * delta.Seconds)
            {
                Audio.Play(0, true, AmbientSound);
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

            if (ShowHealthbar)
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


        /// <summary>
        /// Checks for invinsibility frames before calling damage.
        /// </summary>
        /// <param name="damage"></param>
        /// <param name="direction"></param>
        public virtual void TryHurt(int damage, Angle direction)
        {
            if (HitFrames <= 0)
            {
                Damage(damage, direction);
            }
        }

        public virtual void Damage(int damage, Angle direction)
        {
            Health -= damage;

            HitFrames = maxHitFrames;

            direction.Revolutions += random.Next(-1, 1) / 10;
            ApplyDirecionalVelocity(direction, 500);

            Audio.Play(0.1f, true, HitSound);

            GameWorld.SpawnParticles(random.Next(1, 3), "blood", Position);
        }


        public override void Die()
        {
            Audio.Play(0.1f, true, DeathSound);

			GameWorld.SpawnParticles(random.Next(5, 15), "blood", Position);

            GameWorld.DisolveToParticles(this);

            /*for (int i = 0; i < Gold; i++)
            {
                GameWorld.EntityManager.CreateEntity(new Pickups.Coin(Position));
            }*/

            GameWorld.RollLootTable(Position, Loot);

            base.Die();
        }


        public virtual void UseMain()
        {
            GameWorld.EntityManager.CreateEntity(new MeleeAttack(this, state.Facing, 1));
        }


        public virtual void UseSecondary()
        {
            
        }
    }
}
