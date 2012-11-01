using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace TheY
{
    public interface ITexture2D :IDisposable
    {
        Texture2D Real();

        int Width { get; }

        int Height { get; }
    }
    public class Texture2DAdapter:ITexture2D
    {
        private Texture2D texture;
        public Texture2DAdapter(Texture2D texture)
        {
            this.texture = texture;
        }
        public Texture2D Real() { return texture; }
        public int Width { get { return texture.Width; } }

        public int Height { get { return texture.Height; } }
        public void Dispose()
        {
            texture.Dispose();
        }
    }
    public class SpriteBatchAdapter : ISpriteBatch
    {
        private SpriteBatch self;

        public SpriteBatchAdapter(SpriteBatch self)
        {
            this.self = self;
        }
        public void Draw(ITexture2D myTexture, Vector2 spritePosition, Color color) 
        {
            self.Draw(myTexture.Real(), spritePosition, color);
        }
    }
    public interface ISpriteBatch
    {
        void Draw(ITexture2D myTexture, Vector2 spritePosition, Color color);
    }
    public static class Extensions
    {
        public static ITexture2D ToIf(this Texture2D self)
        {
            return new Texture2DAdapter(self);
        }
        public static ISpriteBatch ToIf(this SpriteBatch self)
        {
            return new SpriteBatchAdapter(self);
        }
        public static double Distance(this Vector2 self, Vector2 other)
        {
            return Vector2.Distance(self, other);
        }
    }
}
