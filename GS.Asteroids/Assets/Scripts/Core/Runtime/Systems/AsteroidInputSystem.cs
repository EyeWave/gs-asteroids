using GS.Asteroids.Core.Entity;
using GS.Asteroids.Core.Interfaces;
using GS.Asteroids.Core.Interfaces.Configuration;
using System;
using Mathf = UnityEngine.Mathf;
using Random = UnityEngine.Random;
using Vector3 = UnityEngine.Vector3;

namespace GS.Asteroids.Core.Systems
{
    internal sealed class AsteroidInputSystem : SystemCollectionProviderBase<IAsteroidInputHandler>, IRefreshable
    {
        private const float TwoPi = Mathf.PI * 2;

        private readonly float _accelerationMin;
        private readonly float _accelerationMax;

        internal AsteroidInputSystem(IAppConfigDataProvider appConfigDataProvider) : base()
        {
            IAsteroidConfig asteroidConfig = appConfigDataProvider?.GetConfig<IAsteroidConfig>() ?? throw new ArgumentNullException(nameof(appConfigDataProvider));
            _accelerationMin = asteroidConfig.AccelerationMin;
            _accelerationMax = asteroidConfig.AccelerationMax;
        }

        public void Refresh()
        {
            foreach (IAsteroidInputHandler entityOfCollection in Collection)
                if (entityOfCollection != null)
                    HandleInput(entityOfCollection);
        }

        private void HandleInput(IAsteroidInputHandler entity)
        {
            if (entity.Acceleration > 0.0f)
                return;

            entity.AngularAcceleration = Random.value < 0.5f ? -1.0f : 1.0f * Random.Range(_accelerationMin, _accelerationMax);
            entity.Acceleration = Random.Range(_accelerationMin, _accelerationMax);
            float direction = Random.Range(0.0f, TwoPi);

            entity.Velocity = entity.Acceleration * new Vector3(Mathf.Cos(direction), Mathf.Sin(direction));
        }
    }
}
