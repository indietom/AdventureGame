using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;

namespace AdventureGame
{
    abstract class GameObject
    {
        public Vector2 pos;
        public Vector2 orgin;

        Point spriteCoords;
        Point size;

        public float scale;
        public float angle;
        public float velX;
        public float velY;
        public float speed;
        public float rotation;
        public float depth;

        public SpriteEffects spriteEffect;

        public Color color;

        public bool destroy;

        public void DrawSprite(SpriteBatch spriteBatch, Texture2D spritesheet)
        {
            spriteBatch.Draw(spritesheet, pos, new Rectangle(spriteCoords.X, spriteCoords.Y, size.X, size.Y), color, rotation, orgin, scale, spriteEffect, depth);
        }

        public void AngleMath()
        {
            velX = ((float)Math.Cos(Globals.DegreeToRadian(angle)) * speed);
            velX = ((float)Math.Sin(Globals.DegreeToRadian(angle)) * speed);
        }

        public float DistanceTo(Vector2 target)
        {
            return (float)Math.Sqrt((pos.X - target.X) * (pos.X - target.X) + (pos.X - target.Y) * (pos.X - target.Y));
        }

        public float GetAimAngle(Vector2 target)
        {
            return (float)Math.Atan2(target.Y - pos.Y, target.X - pos.X);
        }

        public Vector2 Vel()
        {
            return new Vector2(velX, velY);
        }

        public Rectangle HitBox()
        {
            return new Rectangle(0, 0, 0, 0);
        }

        public Point GetSpriteCoords()
        {
            return spriteCoords;
        }

        public Point GetSize()
        {
            return size;
        }

        public void SetSpriteCoords(int x2, int y2)
        {
            spriteCoords = new Point(x2, y2);
        }

        public void SetSize(int w2, int h2)
        {
            size = new Point(w2, h2);
        }

        public void SetSize(int size2)
        {
            size = new Point(size2, size2);
        }
    }
}
