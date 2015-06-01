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

        public short turnCount;
        public short maxTurnCount;
        public short hitCount;
        public short maxHitCount;

        public float orginalSpeed;

        public bool aggro;
        public bool hasDeathAnimation;

        public byte chanceOfLoot;
        public byte typeOfLoot;

        public void HealthUpdate()
        {
            Random random = new Random();

            if(health <= 0)
            {
                chanceOfLoot = (byte)random.Next(5);
                if(chanceOfLoot == 3)
                {
                    typeOfLoot = (byte)random.Next(Globals.maxTypesOfLoot+1);
                }
                if(!hasDeathAnimation) destroy = true;
            }
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
                scale = Globals.Lerp(scale, 1.3f, 0.07f);
            }
        }
    }
}
