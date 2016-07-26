using System.Drawing;

namespace libCBlindness.Phases
{
    public interface IImageGeneratorPhase
    {
        void Apply(int width, int height, GeneratorContext context);
    }
}