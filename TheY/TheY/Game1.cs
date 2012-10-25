using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
using Microsoft.Xna.Framework.Net;
using Microsoft.Xna.Framework.Storage;
using TheY.Primitives;
namespace TheY
{
    public class DrawPrimitives
    {
        public DrawPrimitives(GraphicsDevice device,SpriteBatch batch)
        {
            _emptyTexture = new Texture2D(device, 1, 1, false, SurfaceFormat.Color);
            _emptyTexture.SetData(new[] { Color.White });
            this.batch = batch;
        }

        private Texture2D _emptyTexture;

        public void DrawLine(Color color, Line line)
        {
            Vector2 point1 = line.Start;
            Vector2 point2 = line.End;
            float Layer = 0;
            float angle = (float)Math.Atan2(point2.Y - point1.Y, point2.X - point1.X);
            float length = (point2 - point1).Length();

            batch.Draw(_emptyTexture, point1, null, color,
                       angle, Vector2.Zero, new Vector2(length, 1),
                       SpriteEffects.None, Layer);

        }

        private SpriteBatch batch;
    }


    /// <summary>
    /// This is the main type for your game
    /// </summary>
    public class Game1 : Microsoft.Xna.Framework.Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        private Guy guy;
        private Bird bird;
        DrawPrimitives _drawPrimitives;

        public Game1()
        {
            IsMouseVisible = true;
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
        }

        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        protected override void Initialize()
        {
            //this.GraphicsDevice.Viewport.
            // TODO: Add your initialization logic here
            guy = new Guy();
            bird = new Bird();
            base.Initialize();
            guy.InitPosition(this.GraphicsDevice.Viewport.ToIf());
            Console.WriteLine(string.Format("Width: {1}, Height: {0}", this.GraphicsDevice.Viewport.Height, this.GraphicsDevice.Viewport.Width));
            Console.WriteLine(string.Format("X: {0}, Y: {1}", this.GraphicsDevice.Viewport.X, this.GraphicsDevice.Viewport.Y));
            /*
             Y -> Height: 480, 
             X -> Width: 800
             
             X: 0, Y: 0
             */
            //Start: {X:4 Y:0}, End: {X:793 Y:473}
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);
            guy.Load(Content);
            bird.Load(Content);
            _drawPrimitives = new DrawPrimitives(GraphicsDevice, spriteBatch);
            // TODO: use this.Content to load your game content here
        }

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// all content.
        /// </summary>
        protected override void UnloadContent()
        {
            guy.Dispose();
            bird.Dispose();
            // TODO: Unload any non ContentManager content here
        }

        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Update(GameTime gameTime)
        {

            // Allows the game to exit
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed)
                this.Exit();

            guy.HandleInput();

            guy.UpdateSprite(graphics.GraphicsDevice.Viewport.ToIf(), gameTime);
            // TODO: Add your update logic here
            bird.UpdateSprite(graphics.GraphicsDevice.Viewport, gameTime);
            base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            graphics.GraphicsDevice.Clear(Color.CornflowerBlue);

            // Draw the sprite.
            spriteBatch.Begin(SpriteSortMode.BackToFront, BlendState.AlphaBlend);

            guy.Draw(spriteBatch);
            bird.Draw(spriteBatch);
            
            var viewport = graphics.GraphicsDevice.Viewport;
            _drawPrimitives.DrawLine(Color.Pink, new Line { 
                Start = new Vector2(viewport.X, viewport.Y), 
                End = new Vector2(viewport.Width,viewport.Height) 
            });// accross the screen

            _drawPrimitives.DrawLine(Color.Purple, new Line
            {
                Start = new Vector2(viewport.X, viewport.Height - 10),
                End = new Vector2(viewport.Width, viewport.Height - 10)
            });// bottom line

            /*
             X -> 0, Y -> 0,
             X -> Width: 800, Y -> Height: 480, 
             */
            //Start: {X:0 Y:0}, End: {X:800 Y:480}
            
            spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
