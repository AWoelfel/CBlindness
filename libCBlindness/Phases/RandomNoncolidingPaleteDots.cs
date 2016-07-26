using System;
using System.Drawing;
using System.Linq;

namespace libCBlindness.Phases
{
    
    public class RandomNoncolidingPaleteDots : BaseCircleGenerator
    {

        public override void Apply(int width, int height, GeneratorContext context)
        {
            var fails = 0;

            while(fails < 1000) { 
                var retries = 5;
                Circle target;

                do
                {
                    var rad = context.Rnd.NextFloat(MinCircleSize, MaxCircleSize) / 2;

                    var x = context.Rnd.NextFloat(0, width - rad*2) + rad;
                    var y = context.Rnd.NextFloat(0, height - rad*2) + rad;

                    target = new Circle(x,y, rad);

                    if (context.WillIntersect(target, MinDistance))
                    {
                        retries--;
                    }
                    else
                    {
                        break;
                    }

                } while (retries >= 0);

                if (retries < 0)
                {
                    fails++;
                    Log(fails);
                    continue;
                }

                context.AddCircle(target, context.RandomColorForPixel((int) target.X, (int) target.Y));
            }
        }

        private void Log(int fail)
        {

            if ((fail%10) == 0)
            {
                Console.WriteLine(((int)fail/10));
            }

        }

        public RandomNoncolidingPaleteDots(float minCircleSize, float maxCircleSize, float minDistance = 0) : base(minCircleSize, maxCircleSize, minDistance)
        {
        }
    }
}