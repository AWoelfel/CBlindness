using System.Drawing;

namespace libCBlindness.Phases
{
    public class RandomPositivePaleteDots : BaseCircleGenerator
    {
        public override void Apply(int width, int height, GeneratorContext context)
        {

            for (var i = 0; i < 1000; i++)
            {
                var size = context.Rnd.NextFloat(MinCircleSize, MaxCircleSize);

                var x = context.Rnd.NextFloat(0, width - size);
                var y = context.Rnd.NextFloat(0, height - size);

                context.AddCircle(new Circle(x, y, size / 2, context.RandomPositiveColor));
            }
        }

        public RandomPositivePaleteDots(float minCircleSize, float maxCircleSize, float minDistance = 0) : base(minCircleSize, maxCircleSize, minDistance)
        {

        }
    }
}