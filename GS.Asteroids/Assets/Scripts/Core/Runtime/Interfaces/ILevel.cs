using Vector3 = UnityEngine.Vector3;

namespace GS.Asteroids.Core.Interfaces
{
    public interface ILevel
    {
        float ExtentWidth { get; }
        float ExtentHeight { get; }
        float Width { get; }
        float Height { get; }

        float Top { get; }
        float Bottom { get; }
        float Left { get; }
        float Right { get; }

        Vector3 GetPlayerStartPosition();

        Vector3 GetEnemyStartPosition(float radius);
    }
}
