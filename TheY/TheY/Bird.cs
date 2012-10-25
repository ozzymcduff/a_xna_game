using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Graphics;

namespace TheY
{
    public class Bird:IDisposable
    {
        // Set the coordinates to draw the sprite at.
        public Vector2 spritePosition = Vector2.Zero;

        // Store some information about the sprite's motion.
        Vector2 spriteSpeed = new Vector2(50.0f, 50.0f);
        public Texture2D myTexture;

        public void UpdateSprite(Viewport viewport, GameTime gameTime)
        {
            // Move the sprite by speed, scaled by elapsed time.
            spritePosition +=
                spriteSpeed * (float)gameTime.ElapsedGameTime.TotalSeconds;

            int MaxX =
                viewport.Width - myTexture.Width;
            int MinX = 0;
            int MaxY =
                viewport.Height - myTexture.Height;
            int MinY = 0;

            // Check for bounce.
            if (spritePosition.X > MaxX)
            {
                spriteSpeed.X *= -1;
                spritePosition.X = MaxX;
            }

            else if (spritePosition.X < MinX)
            {
                spriteSpeed.X *= -1;
                spritePosition.X = MinX;
            }

            if (spritePosition.Y > MaxY)
            {
                spriteSpeed.Y *= -1;
                spritePosition.Y = MaxY;
            }

            else if (spritePosition.Y < MinY)
            {
                spriteSpeed.Y *= -1;
                spritePosition.Y = MinY;
            }
        }


        public void Dispose()
        {
            if (null != myTexture)
            {
                myTexture.Dispose();
                myTexture = null;
            }
        }

        internal void Load(Microsoft.Xna.Framework.Content.ContentManager Content)
        {
            myTexture = Content.Load<Texture2D>("bird");
        }

        internal void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(myTexture, spritePosition, Color.White);
        }
    }
}
