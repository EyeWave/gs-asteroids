using GS.Asteroids.Core.Entity;
using GS.Asteroids.Core.Interfaces;
using GS.Asteroids.Core.Interfaces.Configuration;
using GS.Asteroids.Core.Interfaces.GamePlay;
using GS.Asteroids.Core.Objects;
using System;
using Random = UnityEngine.Random;
using Vector3 = UnityEngine.Vector3;

namespace GS.Asteroids.Core.Systems
{
    internal sealed class StarSystem : SystemCollectionProviderBase<ILevelCollidable>, IRefreshable
    {
        private readonly IAsteroidConfig _asteroidConfig;
        private readonly ILevel _level;
        private readonly IEntityProvider _entityProvider;
        private readonly IObjectProvider _objectProvider;

        private DateTime _spawntTime;

        private DateTime Now => DateTime.Now;

        internal StarSystem(
            IAppConfigDataProvider appConfigDataProvider,
            ILevel level,
            IEntityProvider entityProvider,
            IObjectProvider objectProvider) : base()
        {
            _asteroidConfig = appConfigDataProvider?.GetConfig<IAsteroidConfig>() ?? throw new ArgumentNullException(nameof(appConfigDataProvider));
            _level = level ?? throw new ArgumentNullException(nameof(level));
            _entityProvider = entityProvider ?? throw new ArgumentNullException(nameof(entityProvider));
            _objectProvider = objectProvider ?? throw new ArgumentNullException(nameof(objectProvider));
        }

        public override void Init()
        {
            base.Init();

            _spawntTime = GetNextTime(_asteroidConfig.SpawnIntervalSec / _asteroidConfig.UpIntervalSec);
        }

        public void Refresh()
        {
            if (_spawntTime < Now)
            {
                _spawntTime = GetNextTime(_asteroidConfig.SpawnIntervalSec);
                for (int i = 0; i < _asteroidConfig.CountMin; i++)
                    _entityProvider.Add(Create());
            }
        }

        private DateTime GetNextTime(float seconds)
        {
            return Now.AddSeconds(seconds);
        }

        private IEntity Create()
        {
            Bullet @object = _objectProvider.Take<Bullet>();

            @object.Position = new Vector3(Random.Range(_level.Left, _level.Right), _level.Top);
            @object.Velocity = Vector3.down * @object.Acceleration * Random.Range(_asteroidConfig.AccelerationMin, _asteroidConfig.AccelerationMax);

            return @object;
        }
    }
}
