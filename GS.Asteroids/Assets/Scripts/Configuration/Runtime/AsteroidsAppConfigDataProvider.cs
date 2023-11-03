using GS.Asteroids.Core.Interfaces;
using System;
using System.Collections.Generic;
using Vector3 = UnityEngine.Vector3;

namespace GS.Asteroids.Configuration
{
    internal class AsteroidsAppConfigDataProvider : IAppConfigDataProvider
    {
        private readonly AsteroidsAppConfigData _appConfigData;
        private readonly AsteroidsCorePointsGenerator _corePointsGenerator;
        private readonly Dictionary<RuntimeTypeHandle, object> _cacheConfigs;

        internal AsteroidsAppConfigDataProvider(
            AsteroidsAppConfigData appConfigData,
            AsteroidsCorePointsGenerator corePointsGenerator)
        {
            _appConfigData = appConfigData ?? throw new ArgumentNullException(nameof(appConfigData));
            _corePointsGenerator = corePointsGenerator ?? throw new ArgumentNullException(nameof(corePointsGenerator));
            _cacheConfigs = new Dictionary<RuntimeTypeHandle, object>(128);
        }

        public void Dispose()
        {
            _cacheConfigs.Clear();
        }

        public T GetConfig<T>()
        {
            T result = default;
            RuntimeTypeHandle typeHandle = typeof(T).TypeHandle;

            if (_cacheConfigs.TryGetValue(typeHandle, out object @object))
            {
                result = (T)@object;
            }
            else
            {
                result = _appConfigData.GetValue<T>();
                _cacheConfigs.Add(typeHandle, result);
            }

            return result;
        }

        public IReadOnlyList<Vector3> GetCorePointsOfPlayerShip(float radius)
        {
            return _corePointsGenerator.GetCorePointsOfPlayerShip(radius);
        }

        public IReadOnlyList<Vector3> GetCorePointsOfAsteroid(float radius)
        {
            return _corePointsGenerator.GetCorePointsOfAsteroid(radius);
        }

        public IReadOnlyList<Vector3> GetCorePointsOfBullet(float radius)
        {
            return _corePointsGenerator.GetCorePointsOfBullet(radius);
        }
    }
}
