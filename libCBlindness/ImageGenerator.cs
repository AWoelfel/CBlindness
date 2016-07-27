using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using libCBlindness.Phases;

namespace libCBlindness
{
    public class ImageGenerator
    {
        private static bool Trace => true;

        private readonly Image _mask;

        public ImageGenerator(Image mask)
        {
            _mask = mask;
        }

        public ImageDescriptor Generate(params IImageGeneratorPhase[] phases)
        {
            var mask = ImageUtils.ToBnW(_mask);

            if (Trace)
                ImageUtils.SaveImage(mask, new FileInfo(@"d:\tmp\bw.png"));

            var context = new GeneratorContext(mask);

            return context.CreateImage(phases);
        }



    }
}