using GS.Asteroids.Core.Entity;
using GS.Asteroids.Core.Interfaces;

namespace GS.Asteroids.Core.Systems
{
    internal sealed class RefreshLaserDrawablePointsSystem : SystemCollectionProviderBase<ILaserDrawableProvider>, IRefreshable
    {
        internal RefreshLaserDrawablePointsSystem() : base()
        {
        }

        public override void Add(IEntity entity)
        {
            base.Add(entity);

            if (entity is ILaserDrawableProvider entityOfCollection)
                RefreshDrawablePoints(entityOfCollection);
        }

        public void Refresh()
        {
            foreach (ILaserDrawableProvider entityOfCollection in Collection)
                if (entityOfCollection != null)
                    RefreshDrawablePoints(entityOfCollection);
        }

        private void RefreshDrawablePoints(ILaserDrawableProvider entity)
        {
            entity.GetPoints()[0] = entity.Position;
        }
    }
}
