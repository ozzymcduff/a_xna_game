using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace TheY.Primitives
{
    public struct Triangle
    {
        public static Triangle Sample()
        {
            return new Triangle
            {
                A = new Vector3(-0.5f, -0.25f, 0),
                B = new Vector3(0, 0.5f, 0),
                C = new Vector3(0.5f, -0.25f, 0)
            };
        }
        public Vector3 A;
        public Vector3 B;
        public Vector3 C;
        public override string ToString()
        {
            return string.Format("A: {0}, B: {1}, C: {2}", A, B, C);
        }
        public VertexPositionColor[] ToPositions(Color color)
        {
            VertexPositionColor[] tris = new VertexPositionColor[]{
             new VertexPositionColor(A, color),
             new VertexPositionColor(B, color),
             new VertexPositionColor(C, color),
            };
            return tris;
        }
    }
}
