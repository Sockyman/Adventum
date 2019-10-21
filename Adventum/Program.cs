using System;
using System.Xml;
using System.Collections.Generic;
using Adventum.Data;
using Microsoft.Xna.Framework;

namespace Adventum
{
    /// <summary>
    /// The main class.
    /// </summary>
    public static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            using (var game = new Source.Main())
                game.Run();
        }
    }
}
