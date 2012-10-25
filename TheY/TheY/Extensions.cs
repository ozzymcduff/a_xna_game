using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace TheY
{
    public interface IViewport
	{
        Rectangle Bounds
        {
            get;
        }

        int Width { get; }

        int Height { get; }
    }
    public interface ITexture2D :IDisposable
    {
        Texture2D Real();

        int Width { get; }

        int Height { get; }
    }
    public class ViewportAdapter : IViewport
    {
        private Viewport viewport;
        public ViewportAdapter(Viewport viewport)
        {
            this.viewport = viewport;
        }
        public Rectangle Bounds
        {
            get { return viewport.Bounds; }
        }
        public int Width { get { return viewport.Width; } }

        public int Height { get { return viewport.Height; } }
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
        public static IViewport ToIf(this Viewport self)
        {
            return new ViewportAdapter(self);
        }
        public static ITexture2D ToIf(this Texture2D self)
        {
            return new Texture2DAdapter(self);
        }
    }
}
