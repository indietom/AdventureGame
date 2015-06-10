using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Input;

namespace AdventureGame
{
    class TextBox
    {
        const short MAX_CHARECTERS_ON_LINE = 40;

        Vector2 pos;

        Color textColor;

        public string Name { private set; get; }
        string displayText;
        string fullText;

        short addInterval;
        short addCount;
        short currentIndex;

        public bool destroy;

        Point windowSize;

        KeyboardState keyboard;
        KeyboardState prevKeyboard;

        public TextBox(Vector2 pos2, string name2, string fullText2, short addInterval2)
        {
            pos = pos2;
            Name = name2;
            fullText = Name + ":\n" + ProccesText(fullText2);
            displayText = "";
            addInterval = addInterval2;
            windowSize = new Point((int)AssetManager.smallFont.MeasureString(fullText).X / 8, (int)AssetManager.smallFont.MeasureString(fullText).Y / 8); 

            textColor = Color.White;
        }

        public void Update()
        {
            prevKeyboard = keyboard;
            keyboard = Keyboard.GetState();

            foreach (Player p in Game1.gameObjects.Where(item => item is Player))
            {
                p.inputActive = false;
            }

            if (keyboard.IsKeyDown(Keys.X) && !prevKeyboard.IsKeyDown(Keys.X))
            {
                if (fullText.Length != displayText.Length)
                {
                    displayText = fullText;
                }
                else
                {
                    destroy = true;
                    if (Name == "Shop Keeper")
                    {
                        Gui.shop.active = true;
                        Gui.shop.delay = 1;
                    }
                    foreach (Player p in Game1.gameObjects.Where(item => item is Player))
                    {
                        p.inputActive = true;
                        p.inputDelay = 1;
                    }
                }
            }

            if (fullText.Length != displayText.Length)
            {
                addCount += 1;
                if (addCount >= addInterval)
                {
                    displayText += fullText[currentIndex];
                    currentIndex += 1;
                    addCount = 0;
                }
            }
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(AssetManager.spritesheet, new Rectangle((int)pos.X, (int)pos.Y, (windowSize.X+1) * 8, (windowSize.Y * 8)+2), new Rectangle(1, 464, 130, 136), Color.Black);
            for (int x = 0; x < windowSize.X+1; x++)
            {
                spriteBatch.Draw(AssetManager.spritesheet, pos + new Vector2(x * 8, 0), new Rectangle(298, 75, 8, 2), Color.White, 0, Vector2.Zero, 1, SpriteEffects.FlipVertically, 0);
                spriteBatch.Draw(AssetManager.spritesheet, pos + new Vector2(x * 8, (windowSize.Y*8)+2), new Rectangle(298, 75, 8, 2), Color.White);
            }
            for (int y = 0; y < windowSize.Y; y++)
            {
                spriteBatch.Draw(AssetManager.spritesheet, pos + new Vector2(-2, (8*y)+2), new Rectangle(298, 67, 2, 8), Color.White);
                spriteBatch.Draw(AssetManager.spritesheet, pos + new Vector2((windowSize.X+1)*8, (8 * y) + 2), new Rectangle(298, 67, 2, 8), Color.White, 0, Vector2.Zero,1, SpriteEffects.FlipHorizontally, 0);
            }
            spriteBatch.DrawString(AssetManager.smallFont, displayText, pos + new Vector2(2, 2), textColor);  
        }

        public string ProccesText(string text)
        {
            string tmp = "";

            short count = 0;

            for (int i = 0; i < text.Length; i++)
            {
                count += 1;

                tmp += text[i];
                if (count >= MAX_CHARECTERS_ON_LINE && text[i] == ' ')
                {
                    tmp += "\n";
                    count = 0;
                }
            }
            return tmp;
        }
    }
}
