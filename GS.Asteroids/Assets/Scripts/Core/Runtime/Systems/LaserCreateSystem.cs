using GS.Asteroids.Core.Entity;
using GS.Asteroids.Core.Interfaces;
using GS.Asteroids.Core.Interfaces.GamePlay;
using GS.Asteroids.Core.Objects;
using System;
using System.Linq;
using Mathf = UnityEngine.Mathf;
using Vector3 = UnityEngine.Vector3;

namespace GS.Asteroids.Core.Systems
{
    internal sealed class LaserCreateSystem : SystemCollectionProviderBase<IArmourer>
    {
        private readonly ILevel _level;
        private readonly IInputSystem _inputSystem;
        private readonly IEntityProvider _entityProvider;
        private readonly IObjectProvider _objectProvider;
        private readonly float _offLevelPointMultipler;

        private IArmourer _armourer;

        internal LaserCreateSystem(
            ILevel level,
            IInputSystem inputSystem,
            IEntityProvider entityProvider,
            IObjectProvider objectProvider) : base()
        {
            _level = level ?? throw new ArgumentNullException(nameof(level));
            _inputSystem = inputSystem ?? throw new ArgumentNullException(nameof(inputSystem));
            _entityProvider = entityProvider ?? throw new ArgumentNullException(nameof(entityProvider));
            _objectProvider = objectProvider ?? throw new ArgumentNullException(nameof(objectProvider));
            _offLevelPointMultipler = _level.Width + _level.Height;
        }

        public override void Init()
        {
            base.Init();

            _armourer = Collection.Single();
            _inputSystem.AlternativeFire += OnAlternativeFire;
        }

        public override void Dispose()
        {
            base.Dispose();

            _armourer = null;
            _inputSystem.AlternativeFire -= OnAlternativeFire;
        }

        private void OnAlternativeFire()
        {
            _entityProvider.Add(Create());
        }

        private IEntity Create()
        {
            Laser @object = _objectProvider.Take<Laser>();

            @object.Position = _armourer.Position + _armourer.Direction * _armourer.Radius;
            @object.Velocity = _armourer.Direction * @object.Acceleration;
            @object.Tail = GetSideLevelPoint(_armourer.Position, _armourer.Direction);

            Vector3[] points = @object.GetPoints();
            points[0] = @object.Position;
            points[1] = @object.Tail;

            return @object;
        }

        private Vector3 GetSideLevelPoint(Vector3 position, Vector3 direction)
        {
            Vector3 offLevelPoint = position + direction * _offLevelPointMultipler;

            float offLevelDeltaX = offLevelPoint.x - position.x;
            float offLevelDeltaY = offLevelPoint.y - position.y;

            float sideOfLevelX = offLevelDeltaX > 0 ? _level.Right : _level.Left;
            float sideOfLevelY = offLevelDeltaY > 0 ? _level.Top : _level.Bottom;

            if (Mathf.Approximately(offLevelDeltaX, 0.0f))
                return new Vector3(position.x, sideOfLevelY);

            if (Mathf.Approximately(offLevelDeltaY, 0.0f))
                return new Vector3(sideOfLevelX, position.y);

            float deltaX = (sideOfLevelX - position.x) / offLevelDeltaX;
            float deltaY = (sideOfLevelY - position.y) / offLevelDeltaY;

            return deltaX <= deltaY ?
                new Vector3(sideOfLevelX, position.y + deltaX * offLevelDeltaY) :
                new Vector3(position.x + deltaY * offLevelDeltaX, sideOfLevelY);
        }
    }
}
