using GS.Asteroids.Configuration;
using GS.Asteroids.Core.Interfaces;
using GS.Asteroids.DrawSystem;
using GS.Asteroids.InputSystem;
using GS.Asteroids.Level;
using GS.Asteroids.LocalizationSystem;
using GS.Asteroids.Logger;
using GS.Asteroids.UiSystem;
using System.Threading.Tasks;
using UnityEngine;

namespace GS.Asteroids.Root
{
    internal static class AppContextFactory
    {
        internal static async Task<IAppContext> Create(IRoot root, IAppExitProvider appExitProvider, Camera camera)
        {
            UnityResourceLoader unityResourceLoader = new UnityResourceLoader();

            IDebugLogger logger = AsteroidsLoggerFactory.Create();
            IAppConfigDataProvider appConfigDataProvider = await AsteroidsAppConfigDataProviderFactory.Create(unityResourceLoader);
            ILevel level = AsteroidsLevelFactory.Create(camera);
            IDrawSystem drawSystem = AsteroidsDrawSystemFactory.Create();
            IInputSystem inputSystem = AsteroidsInputSystemFactory.Create();
            IUiSystem uiSystem = await AsteroidsUiSystemFactory.Create(unityResourceLoader);
            ILocalizationSystem localizationSystem = AsteroidsLocalizationSystemFactory.Create(logger);

            root.Install(appConfigDataProvider);
            root.Install(inputSystem);
            root.Install(uiSystem);
            root.Install(localizationSystem);
            root.Install(logger);

            logger.Log(level.ToString());

            return new AppContext(
                root,
                appExitProvider,
                appConfigDataProvider,
                level,
                drawSystem,
                inputSystem,
                uiSystem,
                localizationSystem,
                logger);
        }
    }
}
