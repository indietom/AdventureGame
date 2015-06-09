using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace AdventureGame
{
    class Camera
    {
        public Vector2 pos;

        public Vector2 Corner { get { return new Vector2(pos.X - 320, pos.Y - 240); } }

        public float rotation;
        public float zoom;

        public Camera()
        {
            pos = new Vector2(0, 0);
            rotation = 0;
            zoom = 1;
        }
        
        public void FollowTarget(Vector2 pos2)
        {
            pos = pos2;
        }

        public void LerpToTarget(Vector2 pos2, float speed)
        {
            pos = new Vector2(Globals.Lerp(pos.X, pos2.X, speed), Globals.Lerp(pos.Y, pos2.Y, speed));
        }

        public Matrix GetTransform(GraphicsDevice device2)
        {
            return Matrix.CreateTranslation(new Vector3(-pos.X, -pos.Y, 0)) * Matrix.CreateRotationZ(rotation) * 
                Matrix.CreateScale(new Vector3(zoom, zoom, 1)) * Matrix.CreateTranslation(new Vector3(device2.Viewport.Width* 0.5f, device2.Viewport.Height * 0.5f, 0));
        }
    }
}
