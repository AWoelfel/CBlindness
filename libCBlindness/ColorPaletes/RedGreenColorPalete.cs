using System.Collections.Generic;
using System.Drawing;

namespace libCBlindness.ColorPaletes
{
    class RedGreenColorPalete : IColorPalete
    {
        public RedGreenColorPalete()
        {
            var cConverter = new ColorConverter();

            PositiveColors = new List<Color>
            {
                /*
                (Color)cConverter.ConvertFromString("#9CA594"), 
                (Color)cConverter.ConvertFromString("#ACB4A5"), 
                (Color)cConverter.ConvertFromString("#BBB964"), 
                (Color)cConverter.ConvertFromString("#D7DAAA"), 
                (Color)cConverter.ConvertFromString("#E5D57D"), 
                (Color)cConverter.ConvertFromString("#D1D6AF"), 
                */
                Color.White
            };
            NegativeColors = new List<Color>
            {
                (Color) cConverter.ConvertFromString("#F9BB82"),
                (Color) cConverter.ConvertFromString("#EBA170"),
                (Color) cConverter.ConvertFromString("#FCCD84"),
            };
        }

        public IReadOnlyList<Color> PositiveColors { get; }
        public IReadOnlyList<Color> NegativeColors { get; }
    }
}