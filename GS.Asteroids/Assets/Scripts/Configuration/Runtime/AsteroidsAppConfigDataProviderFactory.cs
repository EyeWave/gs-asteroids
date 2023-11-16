using GS.Asteroids.Configuration.Interfaces;
using GS.Asteroids.Core.Interfaces;
using System.Threading.Tasks;

namespace GS.Asteroids.Configuration
{
    public static class AsteroidsAppConfigDataProviderFactory
    {
        public static async Task<IAppConfigDataProvider> Create(IAppConfigDataLoader appConfigDataLoader)
        {
            AsteroidsAppConfigData appConfigData = await appConfigDataLoader.LoadAsync<AsteroidsAppConfigData>(nameof(AsteroidsAppConfigData));
            CorePointsGenerator corePointsGenerator = new CorePointsGenerator();
            return new AsteroidsAppConfigDataProvider(appConfigData, corePointsGenerator);
        }
    }
}
