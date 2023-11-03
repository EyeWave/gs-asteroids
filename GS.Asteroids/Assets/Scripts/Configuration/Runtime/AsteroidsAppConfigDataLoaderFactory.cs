namespace GS.Asteroids.Configuration
{
    internal static class AsteroidsAppConfigDataLoaderFactory
    {
        internal static IAppConfigDataLoader<AsteroidsAppConfigData> Create()
        {
            return new AsteroidsAppConfigDataLoader();
        }
    }
}
