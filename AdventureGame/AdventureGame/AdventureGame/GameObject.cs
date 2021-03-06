﻿using System;
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

        public Vector2 GetCenter()
        {
            if (orgin == Vector2.Zero)
            {
                return new Vector2(pos.X + size.X / 2, pos.Y + size.Y / 2);
            }
            else
            {
                return orgin;
            }
        }

        public Point spriteCoords;
        public Point size;

        public float scale;
        public float angle;
        public float velX;
        public float velY;
        public float speed;
        public float rotation;
        public float depth = 0.1f;

        public byte direction;

        public sbyte health;
        public sbyte maxHealth;

        public short animationCount;
        public short maxAnimationCount;
        public short currentFrame;
        public short minFrame;
        public short maxFrame;

        public SpriteEffects spriteEffect;

        public Color color;

        public bool destroy;

        public Texture2D texture = AssetManager.spritesheet;

        public int Frame(int cell)
        {
            return 32 * cell + cell + 1;
        }

        public int Frame(int cell, int size)
        {
            return size * cell + cell + 1;
        }

        public virtual void Update()
        {
            if (destroy) Game1.gameObjectsToRemove.Add(this);
        }

        public virtual void DrawSprite(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(texture, pos, new Rectangle(spriteCoords.X, spriteCoords.Y, size.X, size.Y), color, Globals.DegreeToRadian(rotation), orgin, scale, spriteEffect, depth);
        }

        public void Animate()
        {
            if(animationCount >= maxAnimationCount)
            {
                currentFrame += 1;
                if(currentFrame >= maxFrame)
                {
                    currentFrame = minFrame;
                }
                animationCount = 0;
            }
        }

        public void AngleMath()
        {
            velX = ((float)Math.Cos(Globals.DegreeToRadian(angle)) * speed);
            velY = ((float)Math.Sin(Globals.DegreeToRadian(angle)) * speed);
        }

        public float DistanceTo(Vector2 target, bool fromCenter)
        {
            Vector2 objectPos = (fromCenter) ? GetCenter() : pos;
            return (float)Math.Sqrt((objectPos.X - target.X) * (objectPos.X - target.X) + (objectPos.Y - target.Y) * (objectPos.Y - target.Y));
        }

        public float GetAimAngle(Vector2 target, bool fromCenter)
        {
            Vector2 objectPos = (fromCenter) ? GetCenter() : pos;
            return (float)Math.Atan2(target.Y - objectPos.Y, target.X - objectPos.X);
        }

        public float ZOrder()
        {
            return (GetCenter().Y / 1000 <= 0.01f) ? 0.01f : GetCenter().Y / 1000;
        }

        public Vector2 Vel()
        {
            return new Vector2(velX, velY);
        }

        public Rectangle HitBox()
        {
            return new Rectangle((int)(pos.X - orgin.X), (int)(pos.Y - orgin.Y), size.X * (int)scale, size.X * (int)scale);
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
