namespace GS.Asteroids.Core.Interfaces.Configuration
{
    public interface IUfoConfig
    {
        float Radius { get; }
        float AccelerationMin { get; }
        float AccelerationMax { get; }
        int CountMax { get; }
        float SpawnIntervalSec { get; }
    }
}
