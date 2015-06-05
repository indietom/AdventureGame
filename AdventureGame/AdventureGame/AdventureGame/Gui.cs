using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;

namespace AdventureGame
{
    class Gui
    {
        internal static List<TextBox> textBoxes = new List<TextBox>();

        public void Update()
        {
            foreach (TextBox t in textBoxes)
            {
                t.Update();
            }
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            foreach(TextBox t in textBoxes)
            {
                t.Draw(spriteBatch);
            }
        }
    }
}
