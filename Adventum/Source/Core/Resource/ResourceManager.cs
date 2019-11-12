using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Adventum.Data;

namespace Adventum.Source.Core.Resource
{
    public static class ResourceManager
    {
        public static ContentManager content;

        //private static string[] texturesToLoad = { };
        private static Dictionary<string, Texture2D> texturesLoaded;
        private static Dictionary<string, SpriteSheet> spriteSheetsLoaded;


        public static void LoadContent(ContentManager content)
        {
            ResourceManager.content = content;

            texturesLoaded = new Dictionary<string, Texture2D>();
            spriteSheetsLoaded = new Dictionary<string, SpriteSheet>();

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
                    Console.WriteLine("Resource: " + rootFolder + "\\" + name + ", failed to load.");
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


        public static SpriteSheet GetSpriteSheet(string name)
        {
            SpriteSheet spriteSheet = LoadItem<SpriteSheet>("SpriteSheet", name, spriteSheetsLoaded);
            if (spriteSheet != default)
            {
                return spriteSheet;
            }

            return new SpriteSheet();
        }
    }
}
