using System;
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


            //_phases.Add(new RandomNoncolidingPaleteDots(10, 16, 0.5f));
            //_phases.Add(new RandomNoncolidingPaleteDots(6, 8, 0.5f));
            _phases.Add(new RandomNoncolidingPaleteDots(4, 6, 0.5f));
            _phases.Add(new RandomNoncolidingPaleteDots(1, 2, 0.5f));
            /*            _phases.Add(new EmptySegmentFillingPaleteDots(6, 8, 0.5f));
                                                _phases.Add(new EmptySegmentFillingPaleteDots(4, 6, 0.5f));
                                                _phases.Add(new EmptySegmentFillingPaleteDots(1, 2, 0.5f));
                                                */
            using (var g = Graphics.FromImage(result))
            {
                var context = new GeneratorContext(g, mask);

                g.InterpolationMode = InterpolationMode.HighQualityBicubic;
                g.CompositingQuality = CompositingQuality.HighQuality;
                g.PixelOffsetMode = PixelOffsetMode.HighQuality;
                g.SmoothingMode = SmoothingMode.HighQuality;
             
                g.Clear(Color.WhiteSmoke);
                   
                

                foreach (var phase in _phases)
                {
                    phase.Apply(mask.Width, mask.Height, context);
                }
            }

            return result;
        }



    }
}