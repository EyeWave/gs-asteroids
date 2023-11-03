using Vector3 = UnityEngine.Vector3;

namespace GS.Asteroids.Core.Entity
{
    internal interface IPlayerInputHandler : IInputHandler
    {
        Vector3 Direction { set; }
    }
}
