using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Text.RegularExpressions;

namespace libCBlindness
{
    public static class Utils
    {
        public static T TakeRandom<T>(this IReadOnlyList<T> list, IRandomGen rnd)
        {
            return list[rnd.NextInt(list.Count)];
        }

        public static float Distance(this Point p, Circle c)
        {
            var dx = p.X - c.X;
            var dy = p.Y - c.Y;

            return (float) Math.Sqrt(dx*dx + dy*dy);

        }

    }
}