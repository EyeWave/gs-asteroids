namespace GS.Asteroids.Core.Interfaces.Configuration
{
    public interface IPlayerConfig
    {
        float Radius { get; }
        float MaxAcceleration { get; }
        float InertionMultipler { get; }
    }
}
