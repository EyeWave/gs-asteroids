using GS.Asteroids.Core.Entity;
using GS.Asteroids.Core.Interfaces;
using GS.Asteroids.Core.Interfaces.Configuration;
using GS.Asteroids.Core.Objects;
using System;
using System.Collections.Generic;
using Mathf = UnityEngine.Mathf;
using Random = UnityEngine.Random;
using Vector3 = UnityEngine.Vector3;

namespace GS.Asteroids.Core.Factories
{
    internal sealed class AsteroidObjectProvider : ObjectProviderBase<Asteroid>
    {
        private const float TwoPi = Mathf.PI * 2;

        private readonly ILevel _level;
        private readonly IAsteroidConfig _asteroidConfig;
        private readonly Func<float, IReadOnlyList<Vector3>> _getCorePoints;

        internal AsteroidObjectProvider(
            ObjectFactory<Asteroid> objectFactory,
            ILevel level,
            IAppConfigDataProvider appConfigDataProvider) : base(objectFactory)
        {
            _level = level ?? throw new ArgumentNullException(nameof(level));
            _asteroidConfig = appConfigDataProvider?.GetConfig<IAsteroidConfig>() ?? throw new ArgumentNullException(nameof(appConfigDataProvider));
            _getCorePoints = appConfigDataProvider.GetCorePointsOfAsteroid;
        }

        protected override void OnTake(Asteroid @object)
        {
            float radius = Random.Range(_asteroidConfig.RadiusMin, _asteroidConfig.RadiusMax);
            float direction = Random.Range(0.0f, TwoPi);
            IReadOnlyList<Vector3> corePoints = _getCorePoints.Invoke(radius);

            @object.Position = GetStartPositionForAsteroid(radius);
            @object.Acceleration = Random.Range(_asteroidConfig.AccelerationMin, _asteroidConfig.AccelerationMax);
            @object.AngularAcceleration = Random.value < 0.5f ? -1.0f : 1.0f * Random.Range(_asteroidConfig.AccelerationMin, _asteroidConfig.AccelerationMax);
            @object.Velocity = @object.Acceleration * new Vector3(Mathf.Cos(direction), Mathf.Sin(direction));
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

        private Vector3 GetStartPositionForAsteroid(float radius)
        {
            return Random.Range(0, 4) switch
            {
                0 => new Vector3(Random.Range(_level.Left - radius, _level.Right + radius), _level.Top + radius), // top
                1 => new Vector3(Random.Range(_level.Left - radius, _level.Right + radius), _level.Bottom - radius), // bottom
                2 => new Vector3(_level.Left - radius, Random.Range(_level.Bottom - radius, _level.Top + radius)), // left
                3 => new Vector3(_level.Right + radius, Random.Range(_level.Bottom - radius, _level.Top + radius)), // right
                _ => throw new ArgumentOutOfRangeException("The level has only 4 sides"),
            };
        }
    }
}
