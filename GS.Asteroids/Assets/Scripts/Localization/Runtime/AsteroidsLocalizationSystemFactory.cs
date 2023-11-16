using GS.Asteroids.Core.Interfaces;

namespace GS.Asteroids.LocalizationSystem
{
    public static class AsteroidsLocalizationSystemFactory
    {
        public static ILocalizationSystem Create(IDebugLogger logger)
        {
            return new AsteroidsSimpleLocalization(logger);
        }
    }
}
