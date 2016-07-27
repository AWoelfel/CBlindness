using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.IO;
using libCBlindness.Phases;

namespace libCBlindness
{
    public class ImageGenerator
    {
        private static bool Trace => true;

        private readonly Image _mask;

        private List<IImageGeneratorPhase> _phases = new List<IImageGeneratorPhase>();

        public ImageGenerator(Image mask)
        {
            _mask = mask;
        }

        public Image Generate()
        {
            var mask = ImageUtils.ToBnW(_mask);

            if (Trace)
                ImageUtils.SaveImage(mask, new FileInfo(@"d:\tmp\bw.png"));

            var result = new Bitmap(mask.Width, mask.Height, PixelFormat.Format32bppArgb);

            _phases.Add(new RandomNoncolidingPaleteDots(10, 20, 0.0f));
            _phases.Add(new RandomNoncolidingPaleteDots(10, 20, 0.0f));
            _phases.Add(new RandomNoncolidingPaleteDots(10, 20, 0.0f));
            _phases.Add(new RandomNoncolidingPaleteDots(10, 20, 0.0f));
            _phases.Add(new RandomNoncolidingPaleteDots(10, 20, 0.0f));
            _phases.Add(new RandomNoncolidingPaleteDots(5, 10, 0.0f));
            _phases.Add(new RandomNoncolidingPaleteDots(5, 10, 0.0f));
            _phases.Add(new RandomNoncolidingPaleteDots(5, 10, 0.0f));
            _phases.Add(new RandomNoncolidingPaleteDots(5, 10, 0.0f));

            var context = new GeneratorContext(mask);

            var image = context.CreateImage(_phases.ToArray());

            using (var g = Graphics.FromImage(result))
            {

                g.InterpolationMode = InterpolationMode.HighQualityBicubic;
                g.CompositingQuality = CompositingQuality.HighQuality;
                g.PixelOffsetMode = PixelOffsetMode.HighQuality;
                g.SmoothingMode = SmoothingMode.HighQuality;
             
                g.Clear(Color.WhiteSmoke);
                  
                image.Apply(g); 
            
            }

            return result;
        }



    }
}