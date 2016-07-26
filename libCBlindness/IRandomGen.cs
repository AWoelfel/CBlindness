namespace libCBlindness
{
    public interface IRandomGen
    {
        float NextFloat();
        float NextFloat(float min, float max);
        float NextFloat(float max);

        int NextInt(int min, int max);
        int NextInt(int max);

    }
}