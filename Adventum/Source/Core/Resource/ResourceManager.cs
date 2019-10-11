using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace Adventum.Source.Core.Resource
{
    public static class ResourceManager
    {
        private static string[] texturesToLoad = { "test", "humanBase" };
        private static Dictionary<string, Texture2D> texturesLoaded;


        public static void LoadContent(ContentManager content)
        {
            texturesLoaded = LoadContentType<Texture2D>(content, "Texture", texturesToLoad);
        }


        private static Dictionary<string, T> LoadContentType<T>(ContentManager content, string rootFolder, string[] filesToLoad)
        {
            Dictionary<string, T> resourcesLoaded = new Dictionary<string, T>();

            foreach (string name in filesToLoad)
            {
                try
                {
                    T resource = content.Load<T>(rootFolder + "/" + name);
                    resourcesLoaded[name] = resource;
                }
                catch (Exception)
                {
                    Console.WriteLine("Resource: " + rootFolder + "/" + name + ", failed to load.");
                }
            }

            return resourcesLoaded;
        }



        public static Texture2D GetTexture(string name)
        {
            if (texturesLoaded.ContainsKey(name))
            {
                return texturesLoaded[name];
            }
            else
            {
                return new Texture2D(Main.graphics.GraphicsDevice, 32, 32);
            }
        }
    }
}
