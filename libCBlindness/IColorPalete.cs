using System.Collections.Generic;
using System.Drawing;

namespace libCBlindness
{
    public interface IColorPalete
    {
        IReadOnlyList<Color> PositiveColors { get; }
        IReadOnlyList<Color> NegativeColors { get; }
    }
}