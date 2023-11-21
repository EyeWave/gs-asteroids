using System;
using System.Collections.Generic;
using Vector3 = UnityEngine.Vector3;

namespace GS.Asteroids.Core.Interfaces
{
    public interface IAppConfigDataProvider : IDisposable
    {
        T GetConfig<T>();

        IReadOnlyList<Vector3> GetCorePointsOfPlayerShip(float radius);

        IReadOnlyList<Vector3> GetCorePointsOfAsteroid(float radius);

        IReadOnlyList<Vector3> GetCorePointsOfChip(float radius);

        IReadOnlyList<Vector3> GetCorePointsOfUfo(float radius);

        IReadOnlyList<Vector3> GetCorePointsOfBullet(float radius);
    }
}
