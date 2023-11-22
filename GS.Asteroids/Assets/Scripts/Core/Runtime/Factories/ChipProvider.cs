using GS.Asteroids.Core.Entity;
using GS.Asteroids.Core.Interfaces;
using GS.Asteroids.Core.Interfaces.Configuration;
using GS.Asteroids.Core.Objects;
using GS.Asteroids.Core.Utils;
using System;
using System.Collections.Generic;
using Mathf = UnityEngine.Mathf;
using Random = UnityEngine.Random;
using Vector3 = UnityEngine.Vector3;

namespace GS.Asteroids.Core.Factories
{
    internal sealed class ChipProvider : ObjectProviderBase<Chip>
    {
        private readonly float _radius;
        private readonly float _acceleration;
        private readonly Func<float, IReadOnlyList<Vector3>> _getCorePoints;

        internal ChipProvider(
            ObjectFactory<Chip> objectFactory,
            IAppConfigDataProvider appConfigDataProvider) : base(objectFactory)
        {
            IChipConfig chipConfig = appConfigDataProvider?.GetConfig<IChipConfig>() ?? throw new ArgumentNullException(nameof(IChipConfig));
            IAsteroidConfig asteroidConfig = appConfigDataProvider.GetConfig<IAsteroidConfig>() ?? throw new ArgumentNullException(nameof(IAsteroidConfig));

            _radius = asteroidConfig.RadiusMin * chipConfig.MultiplierOfAsteroidRadiusMin;
            _acceleration = asteroidConfig.AccelerationMax * chipConfig.MultiplierOfAsteroidAccelerationMax;
            _getCorePoints = appConfigDataProvider.GetCorePointsOfChip;
        }

        protected override void OnTake(Chip @object)
        {
            IReadOnlyList<Vector3> corePoints = _getCorePoints.Invoke(_radius);
            float direction = Random.Range(0.0f, MathUtils.TwoPi);

            @object.Velocity = _acceleration * new Vector3(Mathf.Cos(direction), Mathf.Sin(direction));
            @object.PointsContainer = new PointsContainer(_radius, corePoints);
        }

        protected override void OnReturn(Chip @object)
        {
            @object.Position = Vector3.zero;
            @object.Velocity = Vector3.zero;
            @object.PointsContainer = PointsContainer.Default;
        }
    }
}
