namespace GS.Asteroids.Core.Interfaces.Configuration
{
    public interface IAsteroidConfig
    {
        float RadiusMin { get; }
        float RadiusMax { get; }
        float AccelerationMin { get; }
        float AccelerationMax { get; }
        int CountMin { get; }
        int CountUp { get; }
        float SpawnIntervalSec { get; }
        float UpIntervalSec { get; }
        int Reward { get; }
    }
}
