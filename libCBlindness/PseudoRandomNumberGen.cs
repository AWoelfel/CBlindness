using System;

namespace libCBlindness
{
    class PseudoRandomNumberGen : BaseRandomNumberGen
    {
        private Random rnd;

        public PseudoRandomNumberGen()
        {
            rnd = new Random((int)DateTime.UtcNow.Ticks);
        }

        public override float NextFloat()
        {
            return (float) rnd.NextDouble();
        }
    }
}