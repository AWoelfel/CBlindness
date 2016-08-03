using System.Collections.Generic;
using System.Drawing;
using libCBlindness;

namespace CBlindnessTest
{
    public class DefaultColorPalete : IColorPalete
    {

        private bool ColorEquals(Color a, Color b)
        {
            return Color.FromArgb(255, a).Equals(Color.FromArgb(255, b));
        }

        public IReadOnlyList<Color> DefaultColors = new List<Color>
            {
                Color.FromArgb(255,255,255),
                Color.FromArgb(254,254,254),
                Color.FromArgb(253,253,253),

                Color.FromArgb(252,252,252),
                Color.FromArgb(251,251,251),
                Color.FromArgb(250,250,250)
            };

        public IReadOnlyList<Color> BlackMask = new List<Color>
            {

             Color.FromArgb(249,187,130),
                Color.FromArgb(235,161,112),
                Color.FromArgb(252,205,132)
                /*
                Color.FromArgb(253, 14, 0),
                Color.FromArgb(241, 129, 4),
            Color.FromArgb(236, 179, 38)*/
            };

        public IReadOnlyList<Color> BlueMask = new List<Color>
            {
                Color.DodgerBlue,
                Color.CornflowerBlue,
                Color.DeepSkyBlue,
                Color.LightSkyBlue,
            };

        public IReadOnlyList<Color> PinkMask = new List<Color>
            {
                Color.BlueViolet,
                Color.Violet,
                Color.DarkViolet,
                Color.MediumVioletRed,
                Color.PaleVioletRed,
            };


        public IReadOnlyList<Color> GetPosibleColors(Color maskColor)
        {
        
            if (ColorEquals(maskColor,Color.Black))
                return BlackMask;

            if (ColorEquals(maskColor, Color.Blue))
                return BlueMask;

            if (ColorEquals(maskColor, Color.FromArgb(255,0,255)))
                return PinkMask;

            return DefaultColors;

        }

    }
}