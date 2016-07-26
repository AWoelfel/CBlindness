﻿using System.Drawing;
using System.IO;
using libCBlindness;

namespace CBlindnessTest
{
    class Program
    {
        static void Main(string[] args)
        {
            var gen = new ImageGenerator(Image.FromFile(@"D:\Hearts.png"));

            var result = gen.Generate();

            ImageUtils.SaveImage(result,  new FileInfo(@"D:\HeartsCB.png"));
            

        }


    }
}