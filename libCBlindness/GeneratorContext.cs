using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Security.Cryptography.X509Certificates;
using libCBlindness.ColorPaletes;

namespace libCBlindness
{
    public class GeneratorContext
    {
        private sealed class XYRadEqualityComparer : IEqualityComparer<Circle>
        {
            public bool Equals(Circle x, Circle y)
            {
                if (ReferenceEquals(x, y)) return true;
                if (ReferenceEquals(x, null)) return false;
                if (ReferenceEquals(y, null)) return false;
                if (x.GetType() != y.GetType()) return false;
                return x.X.Equals(y.X) && x.Y.Equals(y.Y) && x.Rad.Equals(y.Rad);
            }

            public int GetHashCode(Circle obj)
            {
                unchecked
                {
                    var hashCode = obj.X.GetHashCode();
                    hashCode = (hashCode * 397) ^ obj.Y.GetHashCode();
                    hashCode = (hashCode * 397) ^ obj.Rad.GetHashCode();
                    return hashCode;
                }
            }
        }

        private HashSet<Circle> Circles { get; } = new HashSet<Circle>(new XYRadEqualityComparer());
        private Bitmap _mask;


        public GeneratorContext(Graphics g, Image mask)
        {
            

            _mask = new Bitmap(mask);
            Rnd = new PseudoRandomNumberGen();
            ColorPalete = new RedGreenColorPalete();
            G = g;
        }

        public IRandomGen Rnd { get; }
        public IColorPalete ColorPalete { get; }

        public Graphics G { get; }

        public Color RandomPositiveColor => ColorPalete.PositiveColors.TakeRandom(Rnd);
        public Color RandomNegativeColor => ColorPalete.NegativeColors.TakeRandom(Rnd);

        public bool WillIntersect(Circle c, float minDistance = 0f)
        {
            return Circles.Any(x => x.Intersect(c, minDistance));
        }

        public float? ResolveMinDistanceToOthers(Point p)
        {
            return
                Circles.Select(c => new {Circle = c, Distance = p.Distance(c) - c.Rad})
                    .OrderBy(x => x.Distance)
                    .FirstOrDefault()?.Distance;
        }

        public bool IsOnMask(int x, int y)
        {
            var col = _mask.GetPixel(x, y);
            var r = Color.Black;
            return col.R == r.R && col.G == r.G && col.B == r.B;
        }

        public Color RandomColorForPixel(int x, int y)
        {
            var tendence = IsOnMask(x, y);

            if (Rnd.NextFloat() < .05)
                tendence = !tendence;


            if (tendence)
            {
                return RandomNegativeColor;
            }
                

            return RandomPositiveColor;
        }

        public void AddCircle(Circle c, Color color)
        {
            Circles.Add(c);
            G.FillEllipse(new SolidBrush(color), c.X- c.Rad, c.Y- c.Rad, c.Rad * 2, c.Rad * 2);
        }


    }
}