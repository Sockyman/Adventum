﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using Microsoft.Xna.Framework;

namespace Adventum.Data.Generator
{
    class Program
    {
        static void Main(string[] args)
        {

            SpriteSheet data = new SpriteSheet();


            data.name = "ooga";
            data.origin = new Point(16, 32);
            data.frameSize = new Point(32);
            data.animations = new Dictionary<string, Animation>();

            Animation ani = new Animation();
            ani.cellOfOrigin = new Point(0);
            ani.frames = 1;
            ani.FPS = 15;

            data.animations["NameOfAnimation"] = ani;

            XmlWriterSettings settings = new XmlWriterSettings();
            settings.Indent = true;

            using (XmlWriter writer = XmlWriter.Create("test.xml", settings))
            {
                Microsoft.Xna.Framework.Content.Pipeline.Serialization.Intermediate.IntermediateSerializer.Serialize(writer, data, null);
            }
        }
    }
}
