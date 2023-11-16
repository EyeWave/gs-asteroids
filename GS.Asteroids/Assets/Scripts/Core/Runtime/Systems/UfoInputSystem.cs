using GS.Asteroids.Core.Entity;
using GS.Asteroids.Core.Interfaces;
using System.Linq;

namespace GS.Asteroids.Core.Systems
{
    internal sealed class UfoInputSystem : DoubleSystemCollectionProviderBase<IUfoInputHandler, IArmourer>, IRefreshable
    {
        private IArmourer _armourer;

        internal UfoInputSystem() : base()
        {
        }

        public override void Dispose()
        {
            _armourer = null;

            base.Dispose();
        }

        public void Refresh()
        {
            if (SecondCollection.Count == 0)
                return;

            _armourer ??= GetArmourer();

            if (_armourer == null)
                return;

            foreach (IUfoInputHandler entityOfCollection in Collection)
                if (entityOfCollection != null)
                    HandleInput(entityOfCollection);
        }

        private IArmourer GetArmourer()
        {
            return SecondCollection.SingleOrDefault();
        }

        private void HandleInput(IUfoInputHandler entity)
        {
            entity.Velocity = entity.Acceleration * (_armourer.Position - entity.Position).normalized;
        }
    }
}
