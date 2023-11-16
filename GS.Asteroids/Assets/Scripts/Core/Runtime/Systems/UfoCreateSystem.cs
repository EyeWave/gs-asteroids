using GS.Asteroids.Core.Entity;
using GS.Asteroids.Core.Interfaces;
using GS.Asteroids.Core.Interfaces.Configuration;
using GS.Asteroids.Core.Interfaces.GamePlay;
using GS.Asteroids.Core.Objects;
using System;
using Mathf = UnityEngine.Mathf;

namespace GS.Asteroids.Core.Systems
{
    internal sealed class UfoCreateSystem : SystemCollectionProviderBase<IUfoInputHandler>, IRefreshable
    {
        private readonly IEntityProvider _entityProvider;
        private readonly IObjectProvider _objectProvider;
        private readonly IUfoConfig _ufoConfig;

        private int _countMax;
        private DateTime _spawntTime;

        private DateTime Now => DateTime.Now;

        internal UfoCreateSystem(
            IAppConfigDataProvider appConfigDataProvider,
            IEntityProvider entityProvider,
            IObjectProvider objectProvider) : base()
        {
            _entityProvider = entityProvider ?? throw new ArgumentNullException(nameof(entityProvider));
            _objectProvider = objectProvider ?? throw new ArgumentNullException(nameof(objectProvider));
            _ufoConfig = appConfigDataProvider?.GetConfig<IUfoConfig>() ?? throw new ArgumentNullException(nameof(_ufoConfig));
        }

        public override void Init()
        {
            base.Init();

            _countMax = Mathf.Max(0, _ufoConfig.CountMax);
            _spawntTime = GetNextTime(_ufoConfig.SpawnIntervalSec);
        }

        public void Refresh()
        {
            if (_spawntTime < Now)
            {
                _spawntTime = GetNextTime(_ufoConfig.SpawnIntervalSec);

                if (Collection.Count < _countMax)
                    _entityProvider.Add(Create());
            }
        }

        private DateTime GetNextTime(float seconds)
        {
            return Now.AddSeconds(seconds);
        }

        private IEntity Create()
        {
            Ufo @object = _objectProvider.Take<Ufo>();

            return @object;
        }
    }
}
