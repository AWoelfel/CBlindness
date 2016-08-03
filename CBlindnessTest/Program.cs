using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using libCBlindness;
using libCBlindness.Phases;

namespace CBlindnessTest
{
    class Program
    {
        static void Main(string[] args)
        {

            var settings = new IImageGeneratorPhase[]
{
                    new RandomNoncolidingPaleteDots(10, 20, 0.0f),
                    new RandomNoncolidingPaleteDots(10, 20, 0.0f),
                    new RandomNoncolidingPaleteDots(10, 20, 0.0f),
                    new RandomNoncolidingPaleteDots(10, 20, 0.0f),
                    new RandomNoncolidingPaleteDots(10, 20, 0.0f),
                    new RandomNoncolidingPaleteDots(5, 10, 0.0f),
                    new RandomNoncolidingPaleteDots(5, 10, 0.0f),
                    new RandomNoncolidingPaleteDots(5, 10, 0.0f),
                    new RandomNoncolidingPaleteDots(5, 10, 0.0f)
};

            var gen = new ImageGenerator(Image.FromFile(@"D:\CardMask.png"));

            /*  
            foreach (var i in Enumerable.Range(0, 25))
            {
                Console.WriteLine($"Generation {i}");
                

                ImageUtils.SaveImage(ImageDescriptor.LoadFromFile(new FileInfo($@"D:\animation{i}.dmp")).CreateImage(), new FileInfo($@"D:\animation{i}.png"));

            }

            return;
            */
            var descriptorSave = new FileInfo(@"D:\CBCardDescriptor");

            ImageDescriptor imageDescriptor;

            if (descriptorSave.Exists && false)
            {
                imageDescriptor = ImageDescriptor.LoadFromFile(descriptorSave);
            }
            else
            {
                imageDescriptor = gen.Generate(settings);
                imageDescriptor.SaveToFile(descriptorSave);
            }

            ImageUtils.SaveImage(imageDescriptor.CreateImage(new DefaultColorPalete()),  new FileInfo(@"D:\CBCardMask_A.png"));
            ImageUtils.SaveImage(imageDescriptor.CreateImage(new RedGreenColorPalete()), new FileInfo(@"D:\CBCardMask_B.png"));


        }


    }
}
