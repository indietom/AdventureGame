using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace AdventureGame
{
    class Projectile : GameObject
    {
        byte type;
        byte damege;
        byte orginalDamege;

        short animationOffset;

        bool enemy;

        public Projectile(Vector2 pos2, float speed2, float angle2, byte damege2, byte type2, bool enemy2)
        {
            pos = pos2;
            speed = speed2;
            angle = angle2;

            damege = damege2;
            type = type2;

            scale = 1;
            color = Color.White;

            enemy = enemy2;

            AssignSprite();
        }

        public override void Update()
        {
            rotation = angle;

            AngleMath();
            pos += Vel();

            if (maxFrame != 0)
            {
                SetSpriteCoords(Frame(currentFrame, size.X) + animationOffset, spriteCoords.Y);
                Animate();
                animationCount += 1;
            }

            foreach (Enemy e in Game1.gameObjects.Where(item => item is Enemy))
            {
                if (e.HitBox().Intersects(HitBox()) && !enemy)
                {
                    e.Hit((sbyte)damege);
                    destroy = true;
                }
            }

            if (type == 1)
            {
                if (currentFrame >= maxFrame - 1)
                {
                    destroy = true;
                }
            }

            base.Update();
        }

        public void AssignSprite()
        {
            switch(type)
            {
                case 0:
                    SetSpriteCoords(232, 1);
                    SetSize(8);
                    break;
                case 1:
                    SetSize(32);

                    minFrame = 7;
                    maxFrame = 17;
                    currentFrame = minFrame;
                    SetSpriteCoords(Frame(currentFrame), 100);
                    maxAnimationCount = 4;
                    break;
            }

            orgin = new Vector2(size.X / 2, size.Y / 2);
        }
    }
}
