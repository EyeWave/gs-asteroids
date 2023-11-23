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
    internal sealed class UfoProvider : ObjectProviderBase<Ufo>
    {
        private readonly float _radius;
        private readonly float _accelerationMin;
        private readonly float _accelerationMax;

        private readonly Func<float, Vector3> _getStartPosition;
        private readonly IReadOnlyList<Vector3> _corePoints;

        internal UfoProvider(
            ObjectFactoryBase<Ufo> objectFactory,
            Func<float, Vector3> getStartPosition,
            IAppConfigDataProvider appConfigDataProvider) : base(objectFactory)
        {
            _getStartPosition = getStartPosition ?? throw new ArgumentNullException(nameof(getStartPosition));

            IUfoConfig config = appConfigDataProvider?.GetConfig<IUfoConfig>() ?? throw new ArgumentNullException(nameof(IUfoConfig));
            _radius = config.Radius;
            _accelerationMin = config.AccelerationMin;
            _accelerationMax = config.AccelerationMax;

            _corePoints = appConfigDataProvider.GetCorePointsOfUfo(_radius);
        }

        protected override void OnTake(Ufo @object)
        {
            @object.Position = _getStartPosition.Invoke(_radius);
            @object.Acceleration = Random.Range(_accelerationMin, _accelerationMax);
            @object.PointsContainer = new PointsContainer(_radius, _corePoints);
        }

        protected override void OnReturn(Ufo @object)
        {
            @object.Position = Vector3.zero;
            @object.Velocity = Vector3.zero;
            @object.Acceleration = 0.0f;
            @object.PointsContainer = PointsContainer.Default;
        }
    }
}
