using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace MarioGame
{
    public class Camera
    {
        private static Rectangle _camRec;
        public static Vector2 Center;
        private Viewport viewport;
        public Vector2 Origin { get; set; }
        public float Zoom { get; set; }
        public float Rotation { get; set; }

        public Camera(Viewport newViewport)
        {
            viewport = newViewport;
            Origin = new Vector2(viewport.Width / 2.0f, viewport.Height / 2.0f);
            Zoom = 1.0f;
        }
        public void Update(Vector2 position, int reallevelWidth, int levelHeight, bool hidden)
        {
            int levelWidth = reallevelWidth-viewport.Width;
            if (hidden)
            {
                levelWidth = reallevelWidth;
                Center = new Vector2(reallevelWidth-viewport.Width/2, viewport.Height/2);
            }
            else
            {
                if (position.X < viewport.Width / 2)
                    Center.X = viewport.Width / 2;
                else if (position.X > levelWidth - (viewport.Width / 2))
                    Center.X = levelWidth - (viewport.Width / 2);
                else
                    Center.X = position.X;

                if (position.Y < viewport.Height / 2)
                    Center.Y = viewport.Height / 2;
                else if (position.Y > levelHeight - (viewport.Height / 2))
                    Center.Y = levelHeight - (viewport.Height / 2);
                else
                    Center.Y = position.Y;
            }
            _camRec = new Rectangle((int)(Center.X - Origin.X), 0, viewport.Width, viewport.Height);
            if (position.X > reallevelWidth-800)
                Game1.hidden = true;
            else
            {
                Game1.hidden = false;
            }
        }

        public Matrix GetViewMatrix(Vector2 parallax)
        {
            // To add parallax, simply multiply it by the position
            return Matrix.CreateTranslation(new Vector3((-Center.X + (viewport.Width / 2))*parallax.X, (-Center.Y + (viewport.Height / 2)) * parallax.Y, 0.0f)) *
                // The next line has a catch. See note below.
                Matrix.CreateTranslation(new Vector3(-Origin, 0.0f)) *
                Matrix.CreateRotationZ(Rotation) *
                Matrix.CreateScale(Zoom, Zoom, 1) *
                Matrix.CreateTranslation(new Vector3(Origin, 0.0f));
        }

        public static Rectangle GetCamRec()
        {
            return _camRec;
        }
    }
}
