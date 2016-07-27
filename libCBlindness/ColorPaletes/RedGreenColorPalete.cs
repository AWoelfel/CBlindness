using System;
using System.Collections.Generic;
using System.Drawing;

namespace libCBlindness.ColorPaletes
{
    class RedGreenColorPalete : IColorPalete
    {
        private static ColorConverter cConverter = new ColorConverter();

        public RedGreenColorPalete()
        {
            
 
        }

        private bool ColorEquals(Color a, Color b)
        {
            return Color.FromArgb(255, a).Equals(Color.FromArgb(255, b));
        }

        public IReadOnlyList<Color> DefaultColors = new List<Color>
            {
                /*
                (Color)cConverter.ConvertFromString("#9CA594"), 
                (Color)cConverter.ConvertFromString("#ACB4A5"), 
                (Color)cConverter.ConvertFromString("#BBB964"), 
                (Color)cConverter.ConvertFromString("#D7DAAA"), 
                (Color)cConverter.ConvertFromString("#E5D57D"), 
                (Color)cConverter.ConvertFromString("#D1D6AF"), 
                */
                (Color)cConverter.ConvertFromString("#FFFFFF"),
                (Color)cConverter.ConvertFromString("#FEFEFE"),
                (Color)cConverter.ConvertFromString("#FDFDFD"),
                (Color)cConverter.ConvertFromString("#FCFCFC"),
                (Color)cConverter.ConvertFromString("#FBFBFB"),
                (Color)cConverter.ConvertFromString("#FAFAFA"),
            };

        public IReadOnlyList<Color> BlackMask = new List<Color>
            {
                (Color) cConverter.ConvertFromString("#F9BB82"),
                (Color) cConverter.ConvertFromString("#EBA170"),
                (Color) cConverter.ConvertFromString("#FCCD84"),
            };

        public IReadOnlyList<Color> GetPosibleColors(Color maskColor)
        {
        
            if (ColorEquals(maskColor,Color.Black))
                return BlackMask;


            return DefaultColors;

        }

    }
}