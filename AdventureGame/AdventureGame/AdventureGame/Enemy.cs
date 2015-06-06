using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace AdventureGame
{
    abstract class Enemy : GameObject
    {
        public sbyte health;

        public byte damege;
        public byte firerate;
        public byte maxFirerate;
        public byte burstInterval;
        public byte amountOfBurstIntervals;
        public byte shootType;

        public short turnCount;
        public short maxTurnCount;
        public short hitCount;
        public short maxHitCount;

        public float orginalSpeed;
        public float shootAngle;
        public float shootSpeed;

        public Vector2 shootPos;

        public bool aggro;
        public bool burstShoot;
        public bool hasDeathAnimation;

        public byte chanceOfLoot;
        public byte typeOfLoot;

        public bool flying;

        public void ShootUpdate()
        {
            if(aggro)
            {
                firerate += 1;
                if(!burstShoot)
                {
                    if(firerate >= maxFirerate)
                    {
                        Game1.gameObjectsToAdd.Add(GetProjectile());
                        firerate = 0;
                    }
                }
                else
                {
                    for(int i = 0; i < amountOfBurstIntervals; i++)
                    {
                        if(firerate == maxFirerate - burstInterval*i)
                        {
                            Game1.gameObjectsToAdd.Add(GetProjectile());
                        }
                    }
                    if(firerate >= maxFirerate)
                    {
                        firerate = 0;
                    }
                }
            }
        }

        public Projectile GetProjectile()
        {
            return new Projectile(pos + shootPos, shootSpeed, shootAngle, damege, shootType, true);
        }

        public void HealthUpdate()
        {
            Random random = new Random();

            if(health <= 0)
            {
                chanceOfLoot = (byte)random.Next(5);
                if(chanceOfLoot == 3)
                {
                    typeOfLoot = (byte)random.Next(Globals.maxTypesOfLoot);
                    Game1.gameObjectsToAdd.Add(new Loot(pos+new Vector2(16, 16), typeOfLoot));
                }
                if(!hasDeathAnimation) destroy = true;
            }
        }

        public void RandomMovment()
        {
            Random random = new Random(); 

            if(speed > 0) turnCount += 1;

            foreach (PushableTile p in Game1.gameObjects.Where(item => item is PushableTile))
            {
                if (p.HitBox().Intersects(HitBox()))
                {
                    pos += new Vector2(p.velX, p.velY);
                }

                if(p.HitBox().Intersects(new Rectangle((int)pos.X, (int)(pos.Y-speed), 32, 32)))
                {
                    direction = 3;
                    turnCount = 0;
                }
                if (p.HitBox().Intersects(new Rectangle((int)pos.X, (int)(pos.Y + speed), 32, 32)))
                {
                    direction = 2;
                    turnCount = 0;
                }
                if (p.HitBox().Intersects(new Rectangle((int)(pos.X+speed), (int)pos.Y, 32, 32)))
                {
                    direction = 1;
                    turnCount = 0;
                }
                if (p.HitBox().Intersects(new Rectangle((int)(pos.X - speed), (int)pos.Y, 32, 32)))
                {
                    direction = 0;
                    turnCount = 0;
                }
            }

            if(turnCount >= maxTurnCount)
            {
                direction = (byte)random.Next(0, 4);
                turnCount = 0;
            }

            if(direction == 0)
            {
                pos.X += speed;
            }
            if(direction == 1)
            {
                pos.X -= speed;
            }
            if (direction == 2)
            {
                pos.Y -= speed;
            }
            if (direction == 3)
            {
                pos.Y += speed;
            }
        }

        public void Hit(sbyte damege2)
        {
            if (hitCount <= 0)
            {
                health -= damege2;
            }
            hitCount = 1;
        }

        public void HitUpdate()
        {
            Random random = new Random();

            if(hitCount >= 1)
            {
                hitCount += 1;
                color = Color.Red;
            }

            if(hitCount >= maxHitCount)
            {
                color = Color.White;
                hitCount = 0;
                speed = orginalSpeed;
            }
            if(hitCount <= 0)
            {
                scale = Globals.Lerp(scale, 1, 0.07f);
            }
            else
            {
                speed = 0;
                scale = Globals.Lerp(scale, 1.1f, 0.07f);
            }
        }
    }
}
