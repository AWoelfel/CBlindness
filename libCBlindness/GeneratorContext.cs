using System;
using System.Drawing;
using System.Linq;
using libCBlindness.ColorPaletes;
using libCBlindness.Phases;

namespace libCBlindness
{
    public class GeneratorContext
    {
        private ImageDescriptor Image;
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
            return Image.Any(x => x.Intersect(c, minDistance));
        }

        public float? ResolveMinDistanceToOthers(Point p)
        {
            return
                Image.Select(c => new {Circle = c, Distance = p.Distance(c) - c.Rad})
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



        public ImageDescriptor CreateImage(params IImageGeneratorPhase[] phases)
        {
            Image = new ImageDescriptor();
            foreach (var imageGeneratorPhase in phases)
            {
                imageGeneratorPhase.Apply(_mask.Width, _mask.Height, this);
            }


            return Image;

        }


        public void AddCircle(Circle lastDrawnCircle)
        {

            if (Image == null)
                throw new NotSupportedException();

            Image.Add(lastDrawnCircle);
        }
    }
}