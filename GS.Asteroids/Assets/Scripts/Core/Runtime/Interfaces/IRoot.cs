namespace GS.Asteroids.Core.Interfaces
{
    public interface IRoot
    {
        void Install<T>(T system) where T : class;
    }
}
