using GS.Asteroids.Core.Interfaces;

namespace GS.Asteroids.Root
{
    internal sealed class AppContext : IAppContext
    {
        public IRoot Root { get; }

        public IAppConfigDataProvider AppConfigDataProvider { get; }

        public ILevel Level { get; }

        public IDrawSystem DrawSystem { get; }

        public IInputSystem InputSystem { get; }

        public IDebugLogger Logger { get; }

        internal AppContext(
            IRoot root,
            IAppConfigDataProvider appConfigDataProvider,
            ILevel level,
            IDrawSystem drawSystem,
            IInputSystem inputSystem,
            IDebugLogger logger)
        {
            Root = root ?? throw new System.ArgumentNullException(nameof(root));
            AppConfigDataProvider = appConfigDataProvider ?? throw new System.ArgumentNullException(nameof(appConfigDataProvider));
            Level = level ?? throw new System.ArgumentNullException(nameof(level));
            DrawSystem = drawSystem ?? throw new System.ArgumentNullException(nameof(drawSystem));
            InputSystem = inputSystem ?? throw new System.ArgumentNullException(nameof(inputSystem));
            Logger = logger ?? throw new System.ArgumentNullException(nameof(logger));
        }
    }
}
