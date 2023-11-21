namespace GS.Asteroids.Core.Interfaces.Configuration
{
    public interface IChipConfig
    {
        int QuantityOnDestroyOfAsteroid { get; }
        float MultiplierOfAsteroidRadiusMin { get; }
        float MultiplierOfAsteroidAccelerationMax { get; }
        int Reward { get; }
    }
}
