namespace GS.Asteroids.Core.Interfaces
{
    public interface IAppContext
    {
        IRoot Root { get; }
        IAppConfigDataProvider AppConfigDataProvider { get; }
        ILevel Level { get; }
        IDrawSystem DrawSystem { get; }
        IInputSystem InputSystem { get; }
        IDebugLogger Logger { get; }
    }
}