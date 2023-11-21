using GS.Asteroids.Core.Entity;
using GS.Asteroids.Core.Interfaces;
using GS.Asteroids.Core.Interfaces.Configuration;
using GS.Asteroids.Core.Objects;
using System;
using System.Collections.Generic;
using Random = UnityEngine.Random;
using Vector3 = UnityEngine.Vector3;

namespace GS.Asteroids.Core.Factories
{
    internal sealed class AsteroidProvider : ObjectProviderBase<Asteroid>
    {
        private readonly IAsteroidConfig _config;
        private readonly Func<float, Vector3> _getStartPosition;
        private readonly Func<float, IReadOnlyList<Vector3>> _getCorePoints;

        internal AsteroidProvider(
            ObjectFactory<Asteroid> objectFactory,
            Func<float, Vector3> getStartPosition,
            IAppConfigDataProvider appConfigDataProvider) : base(objectFactory)
        {
            _getStartPosition = getStartPosition ?? throw new ArgumentNullException(nameof(getStartPosition));
            _config = appConfigDataProvider?.GetConfig<IAsteroidConfig>() ?? throw new ArgumentNullException(nameof(IAsteroidConfig));
            _getCorePoints = appConfigDataProvider.GetCorePointsOfAsteroid;
        }

        protected override void OnTake(Asteroid @object)
        {
            float radius = Random.Range(_config.RadiusMin, _config.RadiusMax);
            IReadOnlyList<Vector3> corePoints = _getCorePoints.Invoke(radius);

            @object.Position = _getStartPosition.Invoke(radius);
            @object.Acceleration = Random.Range(_config.AccelerationMin, _config.AccelerationMax);
            @object.AngularAcceleration = Random.value < 0.5f ? -1.0f : 1.0f * Random.Range(_config.AccelerationMin, _config.AccelerationMax);
            @object.PointsContainer = new PointsContainer(radius, corePoints);
        }

        protected override void OnReturn(Asteroid @object)
        {
            @object.Position = Vector3.zero;
            @object.Velocity = Vector3.zero;
            @object.Rotation = 0.0f;
            @object.Acceleration = 0.0f;
            @object.AngularAcceleration = 0.0f;
            @object.PointsContainer = PointsContainer.Default;
        }
    }
}
