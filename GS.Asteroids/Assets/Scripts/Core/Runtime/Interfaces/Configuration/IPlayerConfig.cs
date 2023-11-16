namespace GS.Asteroids.Core.Interfaces.Configuration
{
    public interface IPlayerConfig
    {
        float Radius { get; }
        float AccelerationMax { get; }
        float InertionMultipler { get; }
    }
}
