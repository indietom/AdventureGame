using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;

namespace AdventureGame
{
    class AssetManager
    {
        public static SpriteFont smallFont, bigFont;

        public static Texture2D spritesheet;

        public static Texture2D[] tilesheets = new Texture2D[Globals.amountOfTilesets];

        public static void Load(ContentManager content)
        {
            for (int i = 0; i < tilesheets.Count(); i++)
            {
                tilesheets[i] = content.Load<Texture2D>("tilesheet" + (i + 1));
            }

            bigFont = content.Load<SpriteFont>("BigFont");
            smallFont = content.Load<SpriteFont>("SmallFont");

            spritesheet = content.Load<Texture2D>("spritesheet");
        }
    }
}
