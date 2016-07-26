namespace libCBlindness
{
    public abstract class BaseRandomNumberGen : IRandomGen
    {
        public abstract float NextFloat();

        public float NextFloat(float min, float max)
        {
            return (max - min)*NextFloat() + min;
        }

        public float NextFloat(float max)
        {
            return NextFloat(0, max);

        }

        public int NextInt(int min, int max)
        {
            return (int) ((max - min) * NextFloat() + min);
        }

        public int NextInt(int max)
        {
            return NextInt(0, max);
        }
    }
}