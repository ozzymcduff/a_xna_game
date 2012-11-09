using System;
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
        class WorldUnderTest:World
        {
            public override void LoadContent(Microsoft.Xna.Framework.Content.ContentManager Content)
            {
            }
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
        class EmptySpriteBatch : ISpriteBatch
        {
            public void Draw(ITexture2D myTexture, Vector2 spritePosition, Color color)
            {
            }
        }
        class SimulatedGameTime
        {
            public SimulatedGameTime()
            {
                current = new TimeSpan(0,0,0,0,0);
            }
            private TimeSpan current;
            public GameTime Increment(TimeSpan elapsed) 
            {
                current = current.Add(elapsed);
                return new GameTime(current, elapsed);
            }
        }

        [Test]
        public void Should_start_at_bottom_middle() 
        {
            var guy = new Guy();
            guy.InitPosition(new Rectangle(0, 0, 100, 100));
            Assert.That(guy.Position, Is.EqualTo(new Vector2(50, 100)));
        }

        [Test]
        public void Should_not_move_by_default()
        {
            var guy = new Guy();
            guy.myTexture = new FakeTexture2D()
            {
                OnHeight = () => 1,
                OnWidth = () => 1
            };
            var bounds = new Rectangle(0, 0, 1000, 1000);
            guy.InitPosition(bounds);
            guy.Handle(new KeyboardState());
            var time = new SimulatedGameTime();
            guy.UpdateSprite(bounds, time.Increment(new TimeSpan(0,0,2)));
            Assert.That(guy.Position, Is.EqualTo(new Vector2(500, 999)));
            guy.UpdateSprite(bounds, time.Increment(new TimeSpan(0, 0, 5)));
            Assert.That(guy.Position, Is.EqualTo(new Vector2(500, 999)));
        }

        [Test]
        public void Should_bounce_at_edge()
        {
            var guy = new Guy();
            var bounds = new Rectangle(0, 0, 1000, 1000);
            guy.InitPosition(bounds);
            guy.Handle(new KeyboardState(new[] { Keys.Down }));
            guy.myTexture = new FakeTexture2D() { 
                OnHeight = ()=> 1,
                OnWidth = ()=> 1
            };
            var positions = new List<Vector2>();
            positions.Add(guy.Position);
            var time = new SimulatedGameTime();
            guy.UpdateSprite(bounds, time.Increment(new TimeSpan(0, 0, 5)));
            positions.Add(guy.Position);
            guy.UpdateSprite(bounds, time.Increment(new TimeSpan(0, 0, 5)));
            positions.Add(guy.Position);
            Assert.That(positions.ToArray(), Is.EquivalentTo(new[] { new Vector2(500, 1000), new Vector2(500, 999), new Vector2(500, 749) }));
        }

        [Test]
        public void The_bird_should_chase_the_guy()
        {
            var bounds = new Rectangle(0, 0, 1000, 1000);
            var world = new WorldUnderTest();
            var bird = world.bird;

            bird.myTexture = new FakeTexture2D()
            {
                OnHeight = () => 1,
                OnWidth = () => 1
            };
            var guy = world.guy;
            guy.myTexture = new FakeTexture2D()
            {
                OnHeight = () => 1,
                OnWidth = () => 1
            };
            world.Initialize(bounds);
            var time = new SimulatedGameTime();
            world.SpriteBatch = new EmptySpriteBatch();
            double lastdistance = 10000;
            for (int i = 0; i < 15; i++)
            {
                world.Update(time.Increment(new TimeSpan(0, 0, 1)));
                var distance = guy.Position.Distance(bird.spritePosition);
                Console.WriteLine(distance);
                Assert.That(distance, Is.LessThanOrEqualTo(lastdistance));
                lastdistance = distance;
            }
        }

    }
}
