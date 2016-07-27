using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using libCBlindness.ColorPaletes;

namespace libCBlindness
{
    public class GeneratorContext
    {
        private List<Circle> Circles { get; set; } = new List<Circle>();
        private Bitmap _mask;


        public GeneratorContext(Image mask)
        {
            

            _mask = new Bitmap(mask);
            Rnd = new PseudoRandomNumberGen();
            ColorPalete = new RedGreenColorPalete();
        }

        public IRandomGen Rnd { get; }
        public IColorPalete ColorPalete { get; }

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

            if (Rnd.NextFloat() < .025)
                tendence = !tendence;


            if (tendence)
            {
                return RandomNegativeColor;
            }
                

            return RandomPositiveColor;
        }

        public void AddCircle(Circle c)
        {
            Circles.Add(c);
        }

        public void Apply(Graphics g)
        {
            foreach (var c in Circles)
            {
                g.FillEllipse(new SolidBrush(c.C), c.X - c.Rad, c.Y - c.Rad, c.Rad * 2, c.Rad * 2);
            }
        }

        public void SaveCirclesToFile(FileInfo file)
        {
            using (var oFile = file.OpenWrite())
            {
                var serializer = new BinaryFormatter();

                serializer.Serialize(oFile, Circles);
            }
        }

        public void LoadCirclesFromFile(FileInfo file)
        {
            using (var iFile = file.OpenRead())
            {
                var serializer = new BinaryFormatter();

                Circles = serializer.Deserialize(iFile) as List<Circle>;
            }
        }

    }
}