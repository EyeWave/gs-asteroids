using GS.Asteroids.Core.Entity;
using GS.Asteroids.Core.Interfaces;
using GS.Asteroids.Core.Interfaces.Configuration;
using System;
using System.Linq;
using Vector3 = UnityEngine.Vector3;

namespace GS.Asteroids.Core.Systems
{
    internal sealed class UfoInputSystem : DoubleSystemCollectionProviderBase<IUfoInputHandler, IArmourer>, IRefreshable
    {
        private readonly float _inertionMultipler;

        private IArmourer _armourer;

        internal UfoInputSystem(IAppConfigDataProvider appConfigDataProvider) : base()
        {
            IUfoConfig config = appConfigDataProvider?.GetConfig<IUfoConfig>() ?? throw new ArgumentNullException(nameof(IUfoConfig));
            _inertionMultipler = config.InertionMultipler;
        }

        public override void Init()
        {
            base.Init();

            _armourer = SecondCollection.Single();
        }

        public override void Dispose()
        {
            base.Dispose();

            _armourer = null;
        }

        public void Refresh()
        {
            if (SecondCollection.Count == 0 || _armourer == null)
                return;

            foreach (IUfoInputHandler entityOfCollection in Collection)
                if (entityOfCollection != null)
                    HandleInput(entityOfCollection);
        }

        private void HandleInput(IUfoInputHandler entity)
        {
            Vector3 newVelocity = entity.Acceleration * (_armourer.Position - entity.Position).normalized;
            entity.Velocity = Vector3.Lerp(entity.Velocity, newVelocity, _inertionMultipler);
        }
    }
}
