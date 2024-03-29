﻿using Adventum.Data;
using Adventum.Core.Resource;
using Adventum.Util;
using Adventum.World;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;

namespace Adventum.Sprite
{
    public class Animator
    {
        public SpriteSheet Sprite { get; private set; }
        public Texture2D SpriteTexture
        {
            get
            {
                return spriteTexture;
            }
            set
            {
                spriteTexture = value;
            }
        }
        private Texture2D spriteTexture;
        public string TextureName { get; private set; }
        public Texture2D[,] TextureGrid
        {
            get
            {
                return ResourceManager.GetTextureSheet(TextureName, Sprite.frameSize);
            }
        }

        public int FrameIndex
        {
            get
            {
                return (int)Math.Floor(FrameNumber);
            }
            set
            {
                FrameNumber = value;
            }
        }
        public float FrameNumber
        {
            get
            {
                return frameNumber;
            }
            set
            {
                frameNumber = value - ((float)Math.Floor(value / ((float)ActiveAnimation.frames)) * ActiveAnimation.frames);
            }
        }
        private float frameNumber;
        public Direction Facing { get; set; }
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


        public Animator(string spriteSheet, string texture)
        {
            Sprite = ResourceManager.GetSpriteSheet(spriteSheet);
            SpriteTexture = ResourceManager.GetTexture(texture);
            TextureName = texture;

            AnimationName = Sprite.defaultAnimation;

            FrameIndex = 0;
        }

        public Animator(SpriteSheet spriteSheet, string texture)
        {
            Sprite = spriteSheet;
            SpriteTexture = ResourceManager.GetTexture(texture);
            TextureName = texture;

            AnimationName = Sprite.defaultAnimation;

            FrameIndex = 0;
        }



        public void Update(DeltaTime delta, States.EntityState state)
        {
            Facing = state.Facing;
            Update(delta);
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
            int tgx = ActiveAnimation.cellOfOrigin.X + FrameIndex + ActiveAnimation.directionMap.GetDirection(Facing).X;
            int tgy = ActiveAnimation.cellOfOrigin.Y + ActiveAnimation.directionMap.GetDirection(Facing).Y;
            return TextureGrid[tgx, tgy];

            /*return Utils.GetTexturePart(textureData, SpriteTexture.Width, new Rectangle(ActiveAnimation.cellOfOrigin.X + (FrameIndex + ActiveAnimation.directionMap.GetDirection(Facing).X) * Sprite.frameSize.X,
                (ActiveAnimation.cellOfOrigin.Y + ActiveAnimation.directionMap.GetDirection(Facing).Y) * Sprite.frameSize.Y, Sprite.frameSize.X, Sprite.frameSize.Y));*/
        }



        public void Draw(SpriteBatch spriteBatch, Vector2 position, Color color, float layerDepth = 0f)
        {
            spriteBatch.Draw(GetTexture(), position - Sprite.origin.ToVector2(), color: color, effects: (SpriteEffects)ActiveAnimation.flipMap[(int)Facing], layerDepth: layerDepth);
        }
    }
}
