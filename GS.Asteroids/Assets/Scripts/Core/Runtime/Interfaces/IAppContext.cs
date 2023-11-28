namespace GS.Asteroids.Core.Interfaces
{
    public interface IAppContext
    {
        IRoot Root { get; }
        IAppExitProvider AppExitProvider { get; }
        IAppConfigDataProvider AppConfigDataProvider { get; }
        ILevel Level { get; }
        IDrawSystem DrawSystem { get; }
        IInputSystem InputSystem { get; }
        IUiSystem UiSystem { get; }
        ILocalizationSystem LocalizationSystem { get; }
        IDebugLogger Logger { get; }
    }
}
