using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Graphics;

namespace TheY
{
    public class Guy:IDisposable
    {
        // Set the coordinates to draw the sprite at.
        public Vector2 Position = Vector2.Zero;

        // Store some information about the sprite's motion.
        Vector2 spriteSpeed = new Vector2(50.0f, 50.0f);
        public ITexture2D myTexture;


        internal void HandleInput()
        {
            Handle(Keyboard.GetState());
        }

        public void Handle(KeyboardState keyboardstate)
        {
            var newspeed = new Vector2(50.0f, 50.0f);
            const float size = 50.0f;
            //|| GamePad.GetState(PlayerIndex.One).DPad.Down == ButtonState.Pressed

            if (keyboardstate.IsKeyDown(Keys.Down))
            {
                spriteSpeed.Y = size;
                spriteSpeed.X = 0;
                return;
            }
            if (keyboardstate.IsKeyDown(Keys.Up))
            {
                spriteSpeed.Y = -1 * size;
                spriteSpeed.X = 0;
                return;
            }
            if (keyboardstate.IsKeyDown(Keys.Left))
            {
                spriteSpeed.Y = 0;
                spriteSpeed.X = -1 * size;
                return;
            }
            if (keyboardstate.IsKeyDown(Keys.Right))
            {
                spriteSpeed.Y = 0;
                spriteSpeed.X = size;
                return;
            }
            spriteSpeed = new Vector2(0f, 0f);
        }

        public void UpdateSprite(Rectangle bounds, GameTime gameTime)
        {
            // Move the sprite by speed, scaled by elapsed time.
            Position +=
                spriteSpeed * (float)gameTime.ElapsedGameTime.TotalSeconds;
            //Console.WriteLine(gameTime.ElapsedGameTime.TotalSeconds);
            int MaxX =
                bounds.Width - myTexture.Width;
            int MinX = 0;
            int MaxY =
                bounds.Height - myTexture.Height;
            int MinY = 0;

            // Check for bounce.
            if (Position.X > MaxX)
            {
                spriteSpeed.X *= -1;
                Position.X = MaxX;
            }

            else if (Position.X < MinX)
            {
                spriteSpeed.X *= -1;
                Position.X = MinX;
            }

            if (Position.Y > MaxY)
            {
                spriteSpeed.Y *= -1;
                Position.Y = MaxY;
            }

            else if (Position.Y < MinY)
            {
                spriteSpeed.Y *= -1;
                Position.Y = MinY;
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
        public string TextureName = "guy";

        internal void Draw(ISpriteBatch spriteBatch)
        {
            spriteBatch.Draw(myTexture, Position, Color.White);
        }

        public void InitPosition(Rectangle bounds)
        {
            this.Position = new Vector2(bounds.Width/2,bounds.Bottom);
        }
    }
}
