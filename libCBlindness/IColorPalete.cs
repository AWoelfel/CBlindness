using System.Collections.Generic;
using System.Drawing;

namespace libCBlindness
{
    public interface IColorPalete
    {
        IReadOnlyList<Color> GetPosibleColors(Color maskColor);
    }
}