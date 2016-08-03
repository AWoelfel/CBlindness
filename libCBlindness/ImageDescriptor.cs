using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using libCBlindness.Phases;

namespace libCBlindness
{

    [Serializable]
    public class ImageDescriptor : List<CircleInfo>
    {
        private readonly Size _imageSize;

        public ImageDescriptor(Size imageSize)
        {
            _imageSize = imageSize;
        }

        public void SaveToFile(FileInfo file)
        {
            using (var oFile = file.OpenWrite())
            {
                var serializer = new BinaryFormatter();

                serializer.Serialize(oFile, this);
            }
        }

        public static ImageDescriptor LoadFromFile(FileInfo file)
        {
            using (var iFile = file.OpenRead())
            {
                var serializer = new BinaryFormatter();

                return serializer.Deserialize(iFile) as ImageDescriptor;
            }
        }

        public void Apply(Graphics g, IColorPalete colorPalete, IRandomGen rnd)
        {
            foreach (var c in this)
            {
                g.FillEllipse(new SolidBrush(colorPalete.GetPosibleColors(c.MaskColor).TakeRandom(rnd)), c.Circle.X - c.Circle.Rad, c.Circle.Y - c.Circle.Rad, c.Circle.Rad * 2, c.Circle.Rad * 2);
            }
        }

        public Image CreateImage(IColorPalete colorPalette)
        {
            var result = new Bitmap(_imageSize.Width, _imageSize.Height, PixelFormat.Format32bppArgb);

            var rnd = new PseudoRandomNumberGen();

            using (var g = Graphics.FromImage(result))
            {

                g.InterpolationMode = InterpolationMode.HighQualityBicubic;
                g.CompositingQuality = CompositingQuality.HighQuality;
                g.PixelOffsetMode = PixelOffsetMode.HighQuality;
                g.SmoothingMode = SmoothingMode.HighQuality;

                g.Clear(Color.WhiteSmoke);

                Apply(g, colorPalette, rnd);

            }

            return result;
        }


        public void Add(Circle c, Color color)
        {
            Add(new CircleInfo (c, color));   
        }

        public bool WillIntersect(Circle c, float minDistance = 0f)
        {
            return this.Any(x => x.Circle.Intersect(c, minDistance));
        }

        public float? ResolveMinDistanceToOthers(Point p)
        {
            return this.Select(c => p.Distance(c.Circle) - c.Circle.Rad ).OrderBy(x => x).FirstOrDefault();
        }
    }

    [Serializable]
    public class CircleInfo
    {
        public CircleInfo(Circle circle, Color maskColor)
        {
            this.circle = circle;
            this.maskColor = maskColor;
        }

        Circle circle;
        private readonly Color maskColor;

        public Circle Circle
        {
            get { return circle; }
        }

        public Color MaskColor
        {
            get { return maskColor; }
        }

    }

}