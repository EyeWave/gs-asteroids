using GS.Asteroids.Core.Entity;
using GS.Asteroids.Core.Interfaces;
using GS.Asteroids.Core.Interfaces.Configuration;
using GS.Asteroids.Core.Objects;
using System;
using System.Collections.Generic;
using Vector3 = UnityEngine.Vector3;

namespace GS.Asteroids.Core.Factories
{
    internal sealed class BulletProvider : ObjectProviderBase<Bullet>
    {
        private readonly IBulletConfig _config;
        private readonly IReadOnlyList<Vector3> _corePoints;

        internal BulletProvider(
            ObjectFactoryBase<Bullet> objectFactory,
            IAppConfigDataProvider appConfigDataProvider) : base(objectFactory)
        {
            _config = appConfigDataProvider?.GetConfig<IBulletConfig>() ?? throw new ArgumentNullException(nameof(IBulletConfig));
            _corePoints = appConfigDataProvider.GetCorePointsOfBullet(_config.Radius);
        }

        protected override void OnTake(Bullet @object)
        {
            @object.Acceleration = _config.Acceleration;
            @object.PointsContainer = new PointsContainer(_config.Radius, _corePoints);
        }

        protected override void OnReturn(Bullet @object)
        {
            @object.Position = Vector3.zero;
            @object.Velocity = Vector3.zero;
            @object.Rotation = 0.0f;
            @object.Acceleration = 0.0f;
            @object.PointsContainer = PointsContainer.Default;
        }
    }
}
