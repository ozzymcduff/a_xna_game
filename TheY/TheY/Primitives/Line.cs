using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace TheY.Primitives
{
    public struct Line
    {
        public Vector2 Start;
        public Vector2 End;
        public override string ToString()
        {
            return string.Format("Start: {0}, End: {1}", Start, End);
        }
    }
}
