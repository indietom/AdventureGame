using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace AdventureGame
{
    class PushableTile : GameObject
    {
        float weight;

        public PushableTile(Vector2 pos2, Point spriteCoords2, Point size2, float weight2)
        {
            pos = pos2;
            weight = weight2;

            spriteCoords = spriteCoords2;
            size = size2;
            scale = 1;
            color = Color.White;
        }

        public override void Update()
        {
            foreach (Projectile p in Game1.gameObjects.Where(item => item is Projectile))
            {
                if(p.HitBox().Intersects(HitBox()))
                {
                    velX = p.velX/15;
                    velY = p.velY/15;
                    p.destroy = true;
                }
            }

            foreach (Bomb b in Game1.gameObjects.Where(item => item is Bomb))
            {
                Rectangle toTileHitBox = new Rectangle((int)(b.pos.X + b.velX), (int)(b.pos.Y + b.velY), b.size.X/2, b.size.Y/2);
                if (toTileHitBox.Intersects(HitBox()))
                {
                    b.velX = 0;
                    b.velY = 0;
                    b.speed = 0;
                }
            }

            foreach (Player p in Game1.gameObjects.Where(item => item is Player))
            {
                Rectangle toTileHitBox = new Rectangle((int)((p.pos.X+7) + p.velX), (int)((p.pos.Y+2) + p.velY), 19, 30);

                if(toTileHitBox.Intersects(HitBox()) && p.moving)
                {
                    velX = p.velX;
                    velY = p.velY;

                    p.friction = 0.49f;
                }
                else
                {
                    p.friction = p.OrginalFriction;
                }
                if(toTileHitBox.Intersects(HitBox()) && !p.moving)
                {
                    velX = p.velX*1.5f;
                    velY = p.velY*1.5f;
                }
            }

            pos += Vel();

            velX *= weight;
            velY *= weight;

            base.Update();
        }
    }
}
