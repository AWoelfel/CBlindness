namespace libCBlindness.Phases
{
    public abstract class BaseCircleGenerator : IImageGeneratorPhase
    {
        protected BaseCircleGenerator(float minCircleSize, float maxCircleSize, float minDistance = 0)
        {
            MinCircleSize = minCircleSize;
            MaxCircleSize = maxCircleSize;
            MinDistance = minDistance;
        }

        protected float MinCircleSize { get; }
        protected float MaxCircleSize { get; }
        protected float MinDistance { get; set; }

        public abstract void Apply(int width, int height, GeneratorContext context);
    }
}