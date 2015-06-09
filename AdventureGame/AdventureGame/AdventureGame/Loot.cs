using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace AdventureGame
{
    class Loot : GameObject
    {
        byte type;

        float cosCount;

        public Loot(Vector2 pos2, byte type2)
        {
            pos = pos2;
            type = type2;

            SetSpriteCoords(Frame(type, 16) + 264, 1);
            SetSize(16);
            scale = 1;
            color = Color.White;
        }

        public override void Update()
        {
            pos.Y += (float)Math.Sin(20 * cosCount + 30);
            cosCount += 0.01f;

            foreach (Player p in Game1.gameObjects.Where(item => item is Player))
            {
                if(p.HitBox().Intersects(HitBox()))
                {
                    switch (type)
                    {
                        case 0:
                            if (p.health + 1 != p.maxHealth + 1) p.health += 1;
                            break;
                        case 1:
                            p.money += 10;
                            break;
                        case 2:
                            p.amountOfArrows += 5;
                            break;
                        case 3:
                            p.amountOfBombs += 1;
                            break;
                        case 4:
                            p.mana += 1;
                            break;
                        case 5:
                            p.amountOfKeys += 1;
                            break;
                    }
                    destroy = true;
                }
            }
            base.Update();
        }
    }
}
