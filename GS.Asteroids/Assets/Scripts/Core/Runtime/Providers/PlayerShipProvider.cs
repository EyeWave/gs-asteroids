using GS.Asteroids.Core.Entity;
using GS.Asteroids.Core.Interfaces;
using GS.Asteroids.Core.Interfaces.Configuration;
using GS.Asteroids.Core.Objects;
using System;
using System.Collections.Generic;
using Vector3 = UnityEngine.Vector3;

namespace GS.Asteroids.Core.Providers
{
    internal sealed class PlayerShipProvider : ObjectProviderBase<PlayerShip>
    {
        private readonly float _radius;
        private readonly Func<Vector3> _startPositionGenerator;
        private readonly IReadOnlyList<Vector3> _corePoints;

        internal PlayerShipProvider(
            Func<PlayerShip> objectGenerator,
            Func<Vector3> startPositionGenerator,
            IAppConfigDataProvider appConfigDataProvider) : base(objectGenerator)
        {
            _startPositionGenerator = startPositionGenerator ?? throw new ArgumentNullException(nameof(startPositionGenerator));
            IPlayerConfig config = appConfigDataProvider?.GetConfig<IPlayerConfig>() ?? throw new ArgumentNullException(nameof(IPlayerConfig));

            _radius = config.Radius;
            _corePoints = appConfigDataProvider.GetCorePointsOfPlayerShip(_radius);
        }

        protected override void OnTake(PlayerShip @object)
        {
            @object.Position = _startPositionGenerator.Invoke();
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
