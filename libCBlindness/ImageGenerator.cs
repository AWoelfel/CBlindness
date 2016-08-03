using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using libCBlindness.Phases;

namespace libCBlindness
{
    public class ImageGenerator
    {
        private readonly Image mask;

        public ImageGenerator(Image mask)
        {
            this.mask = mask;
        }

        public ImageDescriptor Generate(params IImageGeneratorPhase[] phases)
        {
            return new GeneratorContext(mask).CreateImage(phases);
        }
    }
}