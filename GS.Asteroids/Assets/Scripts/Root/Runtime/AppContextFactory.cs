using GS.Asteroids.Configuration;
using GS.Asteroids.Core.Interfaces;
using GS.Asteroids.DrawSystem;
using GS.Asteroids.InputSystem;
using GS.Asteroids.Level;
using GS.Asteroids.Logger;
using System.Threading.Tasks;
using UnityEngine;

namespace GS.Asteroids.Root
{
    internal static class AppContextFactory
    {
        internal static async Task<IAppContext> Create(IRoot root, Camera camera)
        {
            IAppConfigDataProvider appConfigDataProvider = await AsteroidsAppConfigDataProviderFactory.Create();
            ILevel level = AsteroidsLevelFactory.Create(camera);
            IDrawSystem drawSystem = AsteroidsDrawSystemFactory.Create();
            IInputSystem inputSystem = AsteroidsInputSystemFactory.Create();
            IDebugLogger logger = AsteroidsLoggerFactory.Create();

            root.Install(appConfigDataProvider);
            root.Install(inputSystem);
            root.Install(logger);

            logger.Log(level.ToString());

            return new AppContext(
                root,
                appConfigDataProvider,
                level,
                drawSystem,
                inputSystem,
                logger);
        }
    }
}
