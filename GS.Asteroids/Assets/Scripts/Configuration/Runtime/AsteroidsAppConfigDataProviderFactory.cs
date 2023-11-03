using GS.Asteroids.Core.Interfaces;
using System.Threading.Tasks;

namespace GS.Asteroids.Configuration
{
    public static class AsteroidsAppConfigDataProviderFactory
    {
        public static async Task<IAppConfigDataProvider> Create()
        {
            IAppConfigDataLoader<AsteroidsAppConfigData> appConfigDataLoader = AsteroidsAppConfigDataLoaderFactory.Create();
            AsteroidsAppConfigData appConfigData = await appConfigDataLoader.LoadAsync();
            AsteroidsCorePointsGenerator corePointsGenerator = new AsteroidsCorePointsGenerator();

            return new AsteroidsAppConfigDataProvider(appConfigData, corePointsGenerator);
        }
    }
}
