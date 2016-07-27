using System.Drawing;
using System.IO;
using libCBlindness;
using libCBlindness.Phases;

namespace CBlindnessTest
{
    class Program
    {
        static void Main(string[] args)
        {
            var descriptorSave = new FileInfo(@"D:\CBCardDescriptor");

            ImageDescriptor imageDescriptor;

            if (descriptorSave.Exists)
            {
                imageDescriptor = ImageDescriptor.LoadFromFile(descriptorSave);
            }
            else
            {
                var gen = new ImageGenerator(Image.FromFile(@"D:\CardMask.png"));
                imageDescriptor = gen.Generate(new RandomNoncolidingPaleteDots(10, 20, 0.0f),
                                          new RandomNoncolidingPaleteDots(10, 20, 0.0f),
                                          new RandomNoncolidingPaleteDots(10, 20, 0.0f),
                                          new RandomNoncolidingPaleteDots(10, 20, 0.0f),
                                          new RandomNoncolidingPaleteDots(10, 20, 0.0f),
                                          new RandomNoncolidingPaleteDots(5, 10, 0.0f),
                                          new RandomNoncolidingPaleteDots(5, 10, 0.0f),
                                          new RandomNoncolidingPaleteDots(5, 10, 0.0f),
                                          new RandomNoncolidingPaleteDots(5, 10, 0.0f));

                imageDescriptor.SaveToFile(descriptorSave);
            }




            ImageUtils.SaveImage(imageDescriptor.CreateImage(),  new FileInfo(@"D:\CBCardMask.png"));
            

        }


    }
}
