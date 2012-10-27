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
    public static class Extensions
    {
        public static ITexture2D ToIf(this Texture2D self)
        {
            return new Texture2DAdapter(self);
        }
    }
}
