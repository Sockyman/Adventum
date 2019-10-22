using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Adventum.Data;
using Adventum.Source.Core.Resource;
using Adventum.Source.Util;

namespace Adventum.Source.Sprite
{
    public class Animator
    {
        public SpriteSheet Sprite { get; private set; }
        public Texture2D SpriteTexture { get; private set; }
        public int FrameIndex
        {
            get
            {
                return (int)Math.Floor(frameNumber);
            }
            set
            {
                FrameNumber = value;
            }
        }
        private float FrameNumber
        {
            get
            {
                return frameNumber;
            }
            set
            {
                frameNumber = value - ((float)Math.Floor(value / ((float)ActiveAnimation.frames + 1)) * ActiveAnimation.frames);
            }
        }
        private float frameNumber;

        public string AnimationName { get; private set; }
        public Animation ActiveAnimation
        {
            get
            {
                if (Sprite.animations.ContainsKey(AnimationName))
                    return Sprite.animations[AnimationName];
                return Sprite.animations[Sprite.defaultAnimation];
            }
        }


        public Animator(string spriteSheet, Texture2D texture)
        {
            Sprite = ResourceManager.GetSpriteSheet(spriteSheet);
            SpriteTexture = texture;

            AnimationName = Sprite.defaultAnimation;

            FrameIndex = 0;
        }


        public void Update(DeltaTime delta)
        {
            FrameNumber += ActiveAnimation.FPS * delta.Seconds;
        }


        public void ChangeAnimation(string animation)
        {
            FrameIndex = 0;
            AnimationName = animation;
        }
        public void TryChangeAnimation(string animation)
        {
            if (AnimationName != animation)
                ChangeAnimation(animation);
        }


        public Texture2D GetTexture()
        {
            return Utils.GetTexturePart(SpriteTexture, new Rectangle(ActiveAnimation.cellOfOrigin.X + FrameIndex * Sprite.frameSize.X, ActiveAnimation.cellOfOrigin.Y * Sprite.frameSize.Y, 
                Sprite.frameSize.X, Sprite.frameSize.Y));
        }


        public void Draw(SpriteBatch spriteBatch, Vector2 position)
        {
            spriteBatch.Draw(GetTexture(), position - Sprite.origin.ToVector2(), Color.White);
        }
    }
}
