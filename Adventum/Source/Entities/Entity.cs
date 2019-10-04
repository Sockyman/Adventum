using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Adventum.Source.Entities
{
    public class Entity
    {
        public Vector2 Position { get; set; }
        public Texture2D Sprite { get; protected set; }
        

        public Entity(Vector2 position)
        {
            Position = position;
        }


        public virtual void Update()
    }
}
