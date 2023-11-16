using GS.Asteroids.Core.Entity;
using GS.Asteroids.Core.Interfaces;
using GS.Asteroids.Core.Interfaces.Configuration;
using GS.Asteroids.Core.Interfaces.GamePlay;
using GS.Asteroids.Core.Objects;
using System;
using System.Linq;
using Mathf = UnityEngine.Mathf;

namespace GS.Asteroids.Core.Systems
{
    internal sealed class AsteroidCreateSystem : SystemCollectionProviderBase<IAsteroidInputHandler>, IRefreshable
    {
        private readonly IEntityProvider _entityProvider;
        private readonly IObjectProvider _objectProvider;
        private readonly IAsteroidConfig _asteroidConfig;

        private int _countMin;
        private DateTime _upTime;
        private DateTime _spawntTime;

        private DateTime Now => DateTime.Now;

        internal AsteroidCreateSystem(
            IAppConfigDataProvider appConfigDataProvider,
            IEntityProvider entityProvider,
            IObjectProvider objectProvider) : base()
        {
            _entityProvider = entityProvider ?? throw new ArgumentNullException(nameof(entityProvider));
            _objectProvider = objectProvider ?? throw new ArgumentNullException(nameof(objectProvider));
            _asteroidConfig = appConfigDataProvider?.GetConfig<IAsteroidConfig>() ?? throw new ArgumentNullException(nameof(_asteroidConfig));
        }

        public override void Init()
        {
            base.Init();

            _countMin = Mathf.Max(0, _asteroidConfig.CountMin);
            _upTime = GetNextTime(_asteroidConfig.UpIntervalSec);
            _spawntTime = GetNextTime(_asteroidConfig.SpawnIntervalSec);

            _entityProvider.AddRange(Enumerable.Range(0, _countMin)
                .Select(_ => Create()));
        }

        public void Refresh()
        {
            if (_upTime < Now)
            {
                _upTime = GetNextTime(_asteroidConfig.UpIntervalSec);
                _countMin += Mathf.Max(0, _asteroidConfig.CountUp);
            }

            if (_spawntTime < Now)
            {
                _spawntTime = GetNextTime(_asteroidConfig.SpawnIntervalSec);
                if (Collection.Count < _countMin)
                    _entityProvider.Add(Create());
            }
        }

        private DateTime GetNextTime(float seconds)
        {
            return Now.AddSeconds(seconds);
        }

        private IEntity Create()
        {
            Asteroid @object = _objectProvider.Take<Asteroid>();

            return @object;
        }
    }
}
