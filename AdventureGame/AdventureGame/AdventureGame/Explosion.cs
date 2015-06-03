using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace AdventureGame
{
    class Explosion : GameObject
    {
        byte explosionsSize;

        short animationOffset;

        bool dangerous;
        bool enemy;

        public Explosion(Vector2 pos2, byte explosionsSize2, bool dangerous2, bool enemy2, Color color2)
        {
            pos = pos2;
            explosionsSize = explosionsSize2;

            dangerous = dangerous2;
            enemy = enemy2;

            maxAnimationCount = 4;
            scale = 1f;
            color = color2;

            SetSize(explosionsSize2);
            AssignSprite();
        }

        public override void Update()
        {
            SetSpriteCoords(Frame(currentFrame, size.X)+animationOffset, spriteCoords.Y);

            Animate();

            animationCount += 1;

            if(currentFrame >= maxFrame-1)
            {
                destroy = true;
            }

            if (dangerous)
            {
                foreach (Player p in Game1.gameObjects.Where(item => item is Player))
                {
                    if (p.HitBox().Intersects(HitBox()))
                    {
                        p.health = 0;
                    }
                }

                if (!enemy)
                {
                    foreach (Enemy e in Game1.gameObjects.Where(item => item is Enemy))
                    {
                        if (e.HitBox().Intersects(HitBox()))
                        {
                            e.health = 0;
                        }
                    }
                }
            }
            base.Update();
        }

        public void AssignSprite()
        {
            switch (explosionsSize)
            {
                case 32:
                    minFrame = 7;
                    maxFrame = 13;
                    currentFrame = minFrame;

                    SetSpriteCoords(Frame(currentFrame), 166);
                    break;
            }
            if(size.X != 32) animationOffset = (short)(spriteCoords.X - 1);
        }
    }
}
