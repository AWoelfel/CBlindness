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

        public bool WillIntersect(Circle c, float minDistance = 0f)
        {
            return Image.WillIntersect(c, minDistance);
        }

        public float? ResolveMinDistanceToOthers(Point p)
        {
            return Image.ResolveMinDistanceToOthers(p);
        }

        public bool IsOnMask(int x, int y)
        {
            var col = _mask.GetPixel(x, y);
            var r = Color.Black;
            return col.R == r.R && col.G == r.G && col.B == r.B;
        }

        public Color RandomColorForPixel(int x, int y)
        {
            return ColorPalete.GetPosibleColors(_mask.GetPixel(x, y)).TakeRandom(Rnd);
        }



        public ImageDescriptor CreateImage(params IImageGeneratorPhase[] phases)
        {
            Image = new ImageDescriptor(_mask.Size);
            foreach (var imageGeneratorPhase in phases)
            {
                imageGeneratorPhase.Apply(_mask.Width, _mask.Height, this);
            }

            return Image;
        }


        public void AddCircle(Circle circleToAdd, Color c)
        {

            if (Image == null)
                throw new NotSupportedException();

            Image.Add(circleToAdd, c);
        }

        public void AddCircle(Circle circleToAdd)
        {

            if (Image == null)
                throw new NotSupportedException();

            AddCircle(circleToAdd, RandomColorForPixel((int)circleToAdd.X, (int)circleToAdd.Y));
        }

    }

}