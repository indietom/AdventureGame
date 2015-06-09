using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace AdventureGame
{
    class Chest : GameObject
    {
        public bool locked;
        public bool opened;
        bool hasSpawned;

        GameObject[] containings;

        KeyboardState keyboard;

        public Chest(Vector2 pos2, bool locked2, GameObject[] containings2)
        {
            pos = pos2;

            locked = locked2;
            containings = containings2;

            scale = 1f;
            color = Color.White;
            SetSize(24);
            SetSpriteCoords(364, Frame(Convert.ToInt16(locked)));
        }

        public override void Update()
        {
            if (opened)
            {
                SetSpriteCoords(363 + Frame(1, 24), spriteCoords.Y);

                if (!hasSpawned)
                {
                    for (int i = 0; i < containings.Count(); i++)
                    {
                        Game1.gameObjectsToAdd.Add(containings[i]);
                    }
                    hasSpawned = true;
                }
            }
            else
            {
                keyboard = Keyboard.GetState();

                foreach (Player p in Game1.gameObjects.Where(item => item is Player))
                {
                    if (p.inputActive && keyboard.IsKeyDown(Keys.X) && p.HitBox().Intersects(HitBox()))
                    {
                        if (!locked)
                        {
                            opened = true;
                        }

                        if (locked && p.amountOfKeys > 1)
                        {
                            opened = true;
                            p.amountOfKeys -= 1;
                        }
                    }
                }
            }
            base.Update();
        } 
    }
}
