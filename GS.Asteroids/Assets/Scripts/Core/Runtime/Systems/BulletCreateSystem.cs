using GS.Asteroids.Core.Entity;
using GS.Asteroids.Core.Interfaces;
using GS.Asteroids.Core.Interfaces.GamePlay;
using GS.Asteroids.Core.Objects;
using System;
using System.Linq;

namespace GS.Asteroids.Core.Systems
{
    internal sealed class BulletCreateSystem : SystemCollectionProviderBase<IArmourer>
    {
        private readonly IInputSystem _inputSystem;
        private readonly IEntityProvider _entityProvider;
        private readonly IObjectProvider _objectProvider;

        private IArmourer _armourer;

        internal BulletCreateSystem(
            IInputSystem inputSystem,
            IEntityProvider entityProvider,
            IObjectProvider objectProvider) : base()
        {
            _inputSystem = inputSystem ?? throw new ArgumentNullException(nameof(inputSystem));
            _entityProvider = entityProvider ?? throw new ArgumentNullException(nameof(entityProvider));
            _objectProvider = objectProvider ?? throw new ArgumentNullException(nameof(objectProvider));
        }

        public override void Init()
        {
            base.Init();

            _inputSystem.Fire += OnFire;
        }

        public override void Dispose()
        {
            base.Dispose();

            _inputSystem.Fire -= OnFire;
        }

        private void OnFire()
        {
            _entityProvider.Add(Create());
        }

        private IArmourer GetArmourer()
        {
            return Collection.Single();
        }

        private IEntity Create()
        {
            _armourer ??= GetArmourer();

            Bullet @object = _objectProvider.Take<Bullet>();

            @object.Position = _armourer.Position + _armourer.Direction * _armourer.Radius;
            @object.Rotation = _armourer.Rotation;
            @object.Velocity = _armourer.Direction * @object.Acceleration;

            return @object;
        }
    }
}
