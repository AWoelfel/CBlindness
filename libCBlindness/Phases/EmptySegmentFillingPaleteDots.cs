using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Xml;

namespace libCBlindness.Phases
{
    public class EmptySegmentFillingPaleteDots : BaseCircleGenerator
    {
        public EmptySegmentFillingPaleteDots(float minCircleSize, float maxCircleSize, float minDistance = 0) : base(minCircleSize, maxCircleSize, minDistance)
        {
        }

        private Rectangle CalculateWorkingSpace(int width, int height)
        {
            return new Rectangle(0,0, width , height);
        }

        public override void Apply(int width, int height, GeneratorContext context)
        {
            Circle lastDrawnCircle = null;


            foreach (var p in new PixelWalker(CalculateWorkingSpace(width, height)))
            {

                if (lastDrawnCircle != null)
                {
                    if (p.Distance(lastDrawnCircle) <= lastDrawnCircle.Rad)
                    {
                        continue;
                    }
                    else
                    {
                        lastDrawnCircle = null;
                    }
                }


                var minDistance = context.ResolveMinDistanceToOthers(p);

                if (minDistance.HasValue)
                {
                    var targetCircleSize = minDistance.Value - MinDistance;
                    if (targetCircleSize > MinCircleSize)
                    {
                        context.AddCircle(lastDrawnCircle = new Circle(p.X, p.Y, context.Rnd.NextFloat(MinCircleSize, Math.Min(targetCircleSize, MaxCircleSize))));
                    }
                }
            }

        }


        class PixelWalker : IEnumerable<Point>
        {
            private readonly Rectangle space;
            
            public PixelWalker(Rectangle space)
            {
                this.space = space;
            }

            class Walker : IEnumerator<Point>
            {
                private Rectangle _space;
                
                private int x;
                private int y;

                public Walker(Rectangle space)
                {
                    _space = space;
                    Reset();
                }


                public void Dispose()
                {
                }

                public bool MoveNext()
                {
                    x ++;

                    if (x >= _space.Width)
                    {
                        y ++;
                        x = 0;
                    }

                    if (y >= _space.Height)
                    {
                        return false;
                    }

                    return true;
                }

                public void Reset()
                {
                    x = -1;
                    y = 0;
                }

                public Point Current => new Point(_space.X + x, _space.Y + y);

                object IEnumerator.Current => Current;
            }

            public IEnumerator<Point> GetEnumerator()
            {
                return new Walker(space);
            }

            IEnumerator IEnumerable.GetEnumerator()
            {
                return GetEnumerator();
            }
        }

    }
}