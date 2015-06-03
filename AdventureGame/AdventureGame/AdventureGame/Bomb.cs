using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace AdventureGame
{
    class Bomb : GameObject
    {
        byte explosionSize;

        short lifeTime;
        short maxLifeTime;

        float friction;

        bool enemy;
        
        public Bomb(Vector2 pos2, short maxLifeTime2, float angle2, float speed2, float friction2, byte expolsionSize2, bool enemy2)
        {
            pos = pos2;
            angle = angle2;
            speed = speed2;
            friction = friction2;

            explosionSize = expolsionSize2;
            maxLifeTime = maxLifeTime2;

            enemy = enemy2;

            SetSize(16);
            SetSpriteCoords(232, 34);
            scale = 1.0f;
            color = Color.White;

            orgin = new Vector2(size.X / 2, size.Y / 2);
        }

        public override void Update()
        {
            AngleMath();
            pos += Vel();

            velX *= friction;
            velY *= friction;

            rotation += (float)Math.Abs(velX+velY);

            lifeTime += 1;

            if (lifeTime >= maxLifeTime)
            {
                Game1.gameObjectsToAdd.Add(new Explosion(pos-orgin, explosionSize, true, enemy, Color.Red));
                destroy = true;
            }
            base.Update();
        }
    }
}
