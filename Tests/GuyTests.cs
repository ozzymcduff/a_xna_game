﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;
using TheY;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Graphics;
namespace Tests
{
    [TestFixture]
    public class GuyTests
    {
        class FakeViewPort: IViewport
        {
            public Func<Rectangle> OnBounds;
            public Rectangle Bounds { get { return OnBounds(); } }
            public int Width { get { return OnBounds().Width; } }
            public int Height { get { return OnBounds().Height; } }
        }

        class FakeTexture2D : ITexture2D
        {
            public Texture2D Real() { throw new NotImplementedException(); }
            public Func<int> OnWidth;
            public int Width { get { return OnWidth(); } }

            public Func<int> OnHeight;
            public int Height { get { return OnHeight(); } }
            public void Dispose()
            {
                throw new NotImplementedException(); 
            }
        }

        [Test]
        public void Should_start_at_bottom_middle() 
        {
            var guy = new Guy();
            guy.InitPosition(new FakeViewPort { OnBounds = () => new Rectangle(0, 0, 100, 100) });
            Assert.That(guy.Position, Is.EqualTo(new Vector2(50, 100)));
        }

        [Test]
        public void Should_bounce_at_edge()
        {
            var guy = new Guy();
            var viewport = new FakeViewPort { OnBounds = () => new Rectangle(0, 0, 1000, 1000) };
            guy.InitPosition(viewport);
            guy.Handle(new KeyboardState(new[] { Keys.Down }));
            guy.myTexture = new FakeTexture2D() { 
                OnHeight = ()=> 1,
                OnWidth = ()=> 1
            };
            var positions = new List<Vector2>();
            positions.Add(guy.Position);
            guy.UpdateSprite(viewport, new GameTime(new TimeSpan(0, 0, 2), new TimeSpan(0, 0, 5)));
            positions.Add(guy.Position);
            guy.UpdateSprite(viewport, new GameTime(new TimeSpan(0, 0, 8), new TimeSpan(0, 0, 5)));
            positions.Add(guy.Position);
            Assert.That(positions.ToArray(), Is.EquivalentTo(new[] { new Vector2(500, 1000), new Vector2(500, 999), new Vector2(500, 749) }));
        }
    }
}
