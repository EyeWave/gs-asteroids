using GS.Asteroids.Core.Entity;
using GS.Asteroids.Core.Interfaces;
using GS.Asteroids.Core.Interfaces.Configuration;
using GS.Asteroids.Core.Objects;
using System;
using System.Collections.Generic;
using Vector3 = UnityEngine.Vector3;

namespace GS.Asteroids.Core.Factories
{
    internal sealed class PlayerShipObjectProvider : ObjectProviderBase<PlayerShip>
    {
        private readonly ILevel _level;
        private readonly float _radius;
        private readonly IReadOnlyList<Vector3> _corePoints;

        internal PlayerShipObjectProvider(
            ObjectFactory<PlayerShip> objectFactory,
            ILevel level,
            IAppConfigDataProvider appConfigDataProvider) : base(objectFactory)
        {
            _level = level ?? throw new ArgumentNullException(nameof(level));
            IPlayerConfig playerConfig = appConfigDataProvider?.GetConfig<IPlayerConfig>() ?? throw new ArgumentNullException(nameof(appConfigDataProvider));

            _radius = playerConfig.Radius;
            _corePoints = appConfigDataProvider.GetCorePointsOfPlayerShip(_radius);
        }

        protected override void OnTake(PlayerShip @object)
        {
            @object.Position = _level.GetStartPoint();
            @object.PointsContainer = new PointsContainer(_radius, _corePoints);
        }

        protected override void OnReturn(PlayerShip @object)
        {
            @object.Position = Vector3.zero;
            @object.Velocity = Vector3.zero;
            @object.Rotation = 0.0f;
            @object.Direction = Vector3.zero;
            @object.Acceleration = 0.0f;
            @object.AngularAcceleration = 0.0f;
            @object.PointsContainer = PointsContainer.Default;
        }
    }
}
