using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Audio;
using Adventum.Data;
using Adventum.Util;

namespace Adventum.Core.Resource
{
    public static class ResourceManager
    {
        public static ContentManager content;

        //private static string[] texturesToLoad = { };
        private static Dictionary<string, Texture2D> texturesLoaded;
        private static Dictionary<string, SpriteSheet> spriteSheetsLoaded;
        private static Dictionary<string, Texture2D[,]> textureGrids;
        private static Dictionary<string, SpriteFont> fontsLoaded;
        private static Dictionary<string, Effect> shadersLoaded;
        private static Dictionary<string, SoundEffect> soundsLoaded;


        public static void LoadContent(ContentManager content)
        {
            ResourceManager.content = content;

            texturesLoaded = new Dictionary<string, Texture2D>();
            spriteSheetsLoaded = new Dictionary<string, SpriteSheet>();
            textureGrids = new Dictionary<string, Texture2D[,]>();
            fontsLoaded = new Dictionary<string, SpriteFont>();
            shadersLoaded = new Dictionary<string, Effect>();
            soundsLoaded = new Dictionary<string, SoundEffect>();

            texturesLoaded["pixel"] = new Texture2D(Main.graphics.GraphicsDevice, 1, 1);
            texturesLoaded["pixel"].SetData( new Color[] { Color.White } );
        }


        private static Dictionary<string, T> LoadContentType<T>(string rootFolder, string[] filesToLoad)
        {
            Dictionary<string, T> resourcesLoaded = new Dictionary<string, T>();

            foreach (string name in filesToLoad)
            {
                LoadItem<T>(rootFolder, name, resourcesLoaded);
            }

            return resourcesLoaded;
        }


        private static T LoadItem<T>(string rootFolder, string name, Dictionary<string, T> resourceMap)
        {
            if (!resourceMap.ContainsKey(name))
            {
                try
                {
                    T resource = content.Load<T>(rootFolder + "/" + name);
                    resourceMap[name] = resource;
                    return resource;
                }
                catch (Exception)
                {
                    Console.WriteLine("Resource: " + rootFolder + @"\" + name + ", failed to load.");
                    return default(T);
                }
            }
            return resourceMap[name];
        }


        public static Texture2D GetTexture(string name)
        {
            Texture2D texture = LoadItem<Texture2D>("Texture", name, texturesLoaded);
            if (texture != null)
                return texture;
            return new Texture2D(Main.graphics.GraphicsDevice, 32, 32);
        }


        public static SoundEffect GetSound(string name)
        {
            SoundEffect sound = LoadItem<SoundEffect>("Sound", name, soundsLoaded);
            return sound;
        }

        public static SpriteSheet GetSpriteSheet(string name)
        {
            SpriteSheet spriteSheet = LoadItem<SpriteSheet>("SpriteSheet", name, spriteSheetsLoaded);
            if (spriteSheet != default)
            {
                return spriteSheet;
            }

            return new SpriteSheet();
        }

        public static Texture2D[,] GetTextureSheet(string name, Point size)
        {
            if (!textureGrids.ContainsKey(name))
            {
                Texture2D texture = GetTexture(name);
                Color[] textureData = new Color[texture.Width * texture.Height];
                texture.GetData(textureData);

                textureGrids[name] = new Texture2D[texture.Width / size.X, texture.Height / size.Y];

                for (int x = 0; x < texture.Width / size.X; x++)
                    for (int y = 0; y < texture.Height / size.Y; y++)
                    {
                        textureGrids[name][x, y] = Utils.GetTexturePart(textureData, texture.Width, new Rectangle(new Point(x * size.X, y * size.Y), size));
                    }
            }

            return textureGrids[name];
        }

        public static SpriteFont GetFont(string name)
        {
            SpriteFont font = LoadItem<SpriteFont>("Font", name, fontsLoaded);
            if (font != null)
                return font;
            throw new Exception("Font {name} not found");
        }


        public static Effect GetShader(string name)
        {
            Effect shader = LoadItem("Shader", name, shadersLoaded);
            if (shader != null)
                return shader;
            throw new Exception("Shader {name} not found");
        }
    }
}
