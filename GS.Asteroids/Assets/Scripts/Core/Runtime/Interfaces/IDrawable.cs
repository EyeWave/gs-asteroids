using Vector3 = UnityEngine.Vector3;

namespace GS.Asteroids.Core.Interfaces
{
    public interface IDrawable
    {
        Vector3[] GetPoints();
    }
}
