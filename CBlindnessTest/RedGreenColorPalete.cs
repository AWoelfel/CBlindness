using System.Collections.Generic;
using System.Drawing;
using libCBlindness;

namespace CBlindnessTest
{
    public class RedGreenColorPalete : IColorPalete
    {

        private bool ColorEquals(Color a, Color b)
        {
            return Color.FromArgb(255, a).Equals(Color.FromArgb(255, b));
        }

        public IReadOnlyList<Color> DefaultColors = new List<Color>
            {
                Color.FromArgb(156,165,148),
                Color.FromArgb(172,180,165),
                Color.FromArgb(187,185,100),

                Color.FromArgb(215,218,170),
                Color.FromArgb(229,213,125),
                Color.FromArgb(209,214,175),
            };

        public IReadOnlyList<Color> BlackMask = new List<Color>
            {

                Color.FromArgb(253, 14, 0),
                Color.FromArgb(241, 129, 4),
                Color.FromArgb(236, 179, 38)
            };

        public IReadOnlyList<Color> BlueMask = new List<Color>
            {
                Color.FromArgb(252, 205, 132),
                Color.FromArgb(249, 187, 130),
                Color.FromArgb(235, 161, 112),
                Color.FromArgb(252, 205, 132)
            };

        public IReadOnlyList<Color> PinkMask = new List<Color>
            {
            Color.FromArgb(249, 187, 130),
            Color.FromArgb(235, 161, 112),
            Color.FromArgb(252, 205, 132),
            Color.FromArgb(249, 187, 130),
            Color.FromArgb(235, 161, 112)
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