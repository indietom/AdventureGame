﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace AdventureGame
{
    class Inventory
    {
        EquipableItem[,] items = new EquipableItem[10, 5];

        Vector2 pos;

        public bool active = true;

        Point currentSelect;

        KeyboardState keyboard;
        KeyboardState prevKeyboard;

        public Inventory()
        {
            Random random = new Random();

            pos = new Vector2(16, 16);

            for (int i = 0; i < 5; i++)
            {
                items[random.Next(9), random.Next(4)] = new EquipableItem("test2.txt");
            }

            items[2, 1] = new EquipableItem("test.txt");
        }

        public void Input()
        {
            prevKeyboard = keyboard;
            keyboard = Keyboard.GetState();

            if (items[currentSelect.X, currentSelect.Y] != null)
            {
                foreach (Player p in Game1.gameObjects.Where(item => item is Player))
                {
                    if (keyboard.IsKeyDown(Keys.X) && !prevKeyboard.IsKeyDown(Keys.X))
                    {
                        if (items[currentSelect.X, currentSelect.Y] != p.equipedItems[1])
                        {
                            p.equipedItems[0] = items[currentSelect.X, currentSelect.Y];
                        }
                    }
                    if (keyboard.IsKeyDown(Keys.Z) && !prevKeyboard.IsKeyDown(Keys.Z))
                    {
                        if (items[currentSelect.X, currentSelect.Y] != p.equipedItems[0])
                        {
                            p.equipedItems[1] = items[currentSelect.X, currentSelect.Y];
                        }
                    }
                }
            }

            if (keyboard.IsKeyDown(Keys.Left) && !prevKeyboard.IsKeyDown(Keys.Left) && currentSelect.X != 0)
            {
                currentSelect.X -= 1;
            }
            
            if (keyboard.IsKeyDown(Keys.Right) && !prevKeyboard.IsKeyDown(Keys.Right) && currentSelect.X != items.GetLength(0)-1)
            {
                currentSelect.X += 1;
            }
            if (keyboard.IsKeyDown(Keys.Up) && !prevKeyboard.IsKeyDown(Keys.Up) && currentSelect.Y != 0)
            {
                currentSelect.Y -= 1;
            }
            if (keyboard.IsKeyDown(Keys.Down) && !prevKeyboard.IsKeyDown(Keys.Down) && currentSelect.Y != items.GetLength(1) - 1)
            {
                currentSelect.Y += 1;
            }

            if (keyboard.IsKeyDown(Keys.Q) && !prevKeyboard.IsKeyDown(Keys.Q))
            {
                active = false;
                foreach (Player p in Game1.gameObjects.Where(item => item is Player))
                {
                    p.inputDelay = 1;
                    p.inputActive = true;
                }
            }
        }

        public void Update()
        {
            if (active)
            {
                foreach(Player p in Game1.gameObjects.Where(item => item is Player))
                {
                    p.inputActive = false;
                }
                Input();

                for (int y = 0; y < items.GetLength(1); y++)
                {
                    for (int x = 0; x < items.GetLength(0); x++)
                    {
                        if (items[x, y] != null)
                        {
                            if (items[currentSelect.X, currentSelect.Y] == items[x, y])
                            {
                                items[x, y].iconScale = Globals.Lerp(items[x, y].iconScale, 0.7f, 0.1f);
                                items[x, y].color = new Color(205, 205, 205);
                            }
                            else
                            {
                                items[x, y].iconScale = Globals.Lerp(items[x, y].iconScale, 1, 0.2f);
                                items[x, y].color = Color.White;
                            }
                        }
                    }
                }
            }
            else
            {
                
            }
        }

        public void Draw(SpriteBatch spritebatch)
        {
            if (active)
            {
                spritebatch.Draw(AssetManager.spritesheet, pos + new Vector2((currentSelect.X * 64)-16, (currentSelect.Y * 64)-16), new Rectangle(364, 67, 32, 32), Color.White);
                for (int y = 0; y < items.GetLength(1); y++)
                {
                    for (int x = 0; x < items.GetLength(0); x++)
                    {
                        if (items[x, y] != null)
                        {
                            spritebatch.Draw(AssetManager.spritesheet, pos + new Vector2(x * 64, y * 64), new Rectangle(items[x, y].IconSpirteCoords.X, items[x, y].IconSpirteCoords.Y, 32, 32), items[x, y].color, 0, new Vector2(16, 16), items[x, y].iconScale, SpriteEffects.None, 0);
                        }
                    }
                }
            }
        }
    }
}
