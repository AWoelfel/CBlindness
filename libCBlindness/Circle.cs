using System;
using System.Collections.Generic;
using System.Drawing;
using libCBlindness.Phases;

namespace libCBlindness
{
    [Serializable]
    public class Circle
    {
        public Circle(float x, float y, float rad, Color c)
        {
            X = x;
            Y = y;
            Rad = rad;
            C = c;
        }

        public float X { get; }
        public float Y { get; }
        public float Rad { get; }
        public Color C { get; set; }

        public bool Intersect(Circle c, float minDistance = 0f)
        {
            var dx = c.X - X;
            var dy = c.Y - Y;

            var distance = Math.Sqrt(dx*dx + dy*dy);
            var threshold = c.Rad + Rad + minDistance;

            return !(distance > threshold);
        }

        public override string ToString()
        {
            return $"{X}/{Y}-{Rad}";
        }
    }
}