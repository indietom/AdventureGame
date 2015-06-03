using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace AdventureGame
{
    class Globals
    {
        public static byte maxTypesOfLoot = 5;

        public static float Lerp(float s, float e, float t)
        {
            return s + t * (e - s);
        }

        public static float DegreeToRadian(float degree)
        {
            return degree*(float)Math.PI/180;
        }

        public static float RadianToDegree(float radian)
        {
            return radian*180/(float)Math.PI;
        }
    }
}
