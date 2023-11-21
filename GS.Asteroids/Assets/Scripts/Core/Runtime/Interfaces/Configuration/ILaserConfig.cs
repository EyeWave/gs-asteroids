namespace GS.Asteroids.Core.Interfaces.Configuration
{
    public interface ILaserConfig
    {
        float Radius { get; }
        float Acceleration { get; }
        int CountMax { get; }
        float CoolDownIntervalSec { get; }
    }
}
