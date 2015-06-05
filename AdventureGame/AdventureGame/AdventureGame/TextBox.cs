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

        string name;
        string displayText;
        string fullText;

        short addInterval;
        short addCount;
        short currentIndex;
        short jumpLineCount;
        short textLengthOffset;

        public TextBox(Vector2 pos2, string name2, string fullText2, short addInterval2)
        {
            pos = pos2;
            name = name2;
            fullText = fullText2;
            displayText = "";
            addInterval = addInterval2;

            textColor = Color.White;
        }

        public void Update()
        {
            if (fullText.Length + textLengthOffset != displayText.Length)
            {
                addCount += 1;
                if (addCount >= addInterval)
                {
                    displayText += fullText[currentIndex];

                    jumpLineCount += 1;

                    if (jumpLineCount >= MAX_CHARECTERS_ON_LINE && fullText[currentIndex] == ' ')
                    {
                        displayText += "\n";
                        jumpLineCount = 0;
                        textLengthOffset += 1;
                    }
                    currentIndex += 1;
                    addCount = 0;
                }
            }
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.DrawString(AssetManager.smallFont, displayText, pos, textColor);  
        }
    }
}
