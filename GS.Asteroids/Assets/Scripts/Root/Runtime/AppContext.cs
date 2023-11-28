using GS.Asteroids.Core.Interfaces;
using System;

namespace GS.Asteroids.Root
{
    internal sealed class AppContext : IAppContext
    {
        public IRoot Root { get; }
        public IAppExitProvider AppExitProvider { get; }
        public IAppConfigDataProvider AppConfigDataProvider { get; }
        public ILevel Level { get; }
        public IDrawSystem DrawSystem { get; }
        public IInputSystem InputSystem { get; }
        public IUiSystem UiSystem { get; }
        public ILocalizationSystem LocalizationSystem { get; }
        public IDebugLogger Logger { get; }

        internal AppContext(
            IRoot root,
            IAppExitProvider appExitProvider,
            IAppConfigDataProvider appConfigDataProvider,
            ILevel level,
            IDrawSystem drawSystem,
            IInputSystem inputSystem,
            IUiSystem uiSystem,
            ILocalizationSystem localizationSystem,
            IDebugLogger logger)
        {
            Root = root ?? throw new ArgumentNullException(nameof(root));
            AppExitProvider = appExitProvider ?? throw new ArgumentNullException(nameof(appExitProvider));
            AppConfigDataProvider = appConfigDataProvider ?? throw new ArgumentNullException(nameof(appConfigDataProvider));
            Level = level ?? throw new ArgumentNullException(nameof(level));
            DrawSystem = drawSystem ?? throw new ArgumentNullException(nameof(drawSystem));
            InputSystem = inputSystem ?? throw new ArgumentNullException(nameof(inputSystem));
            UiSystem = uiSystem ?? throw new ArgumentNullException(nameof(uiSystem));
            LocalizationSystem = localizationSystem ?? throw new ArgumentNullException(nameof(localizationSystem));
            Logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }
    }
}
