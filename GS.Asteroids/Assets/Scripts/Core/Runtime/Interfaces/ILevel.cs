using Vector3 = UnityEngine.Vector3;

namespace GS.Asteroids.Core.Interfaces
{
    public interface ILevel
    {
        Vector3 GetStartPoint();

        float ExtentWidth { get; }
        float ExtentHeight { get; }
        float Width { get; }
        float Height { get; }

        float Top { get; }
        float Bottom { get; }
        float Left { get; }
        float Right { get; }
    }
}
