using GS.Asteroids.Core.Entity;
using GS.Asteroids.Core.Interfaces;
using GS.Asteroids.Core.Interfaces.Configuration;
using GS.Asteroids.Core.Objects;
using System;
using System.Collections.Generic;
using Vector3 = UnityEngine.Vector3;

namespace GS.Asteroids.Core.Factories
{
    internal sealed class LaserProvider : ObjectProviderBase<Laser>
    {
        private readonly ILaserConfig _config;
        private readonly IReadOnlyList<Vector3> _corePoints;

        internal LaserProvider(
            ObjectFactoryBase<Laser> objectFactory,
            IAppConfigDataProvider appConfigDataProvider) : base(objectFactory)
        {
            _config = appConfigDataProvider?.GetConfig<ILaserConfig>() ?? throw new ArgumentNullException(nameof(ILaserConfig));
        }

        protected override void OnTake(Laser @object)
        {
            @object.Acceleration = _config.Acceleration;
            @object.PointsContainer = new PointsContainer
            (
                radius: _config.Radius,
                corePoints: new Vector3[] { Vector3.zero, Vector3.zero }
            );
        }

        protected override void OnReturn(Laser @object)
        {
            @object.Position = Vector3.zero;
            @object.Velocity = Vector3.zero;
            @object.Acceleration = 0.0f;
            @object.Tail = Vector3.zero;
            @object.PointsContainer = PointsContainer.Default;
        }
    }
}
