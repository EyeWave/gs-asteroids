using GS.Asteroids.Core.Entity;
using GS.Asteroids.Core.Interfaces;

namespace GS.Asteroids.Core.Systems
{
    internal sealed class RotateSystem : SystemCollectionProviderBase<IRotatable>, IRefreshable
    {
        internal RotateSystem() : base()
        {
        }

        public void Refresh()
        {
            foreach (IRotatable entityOfCollection in Collection)
                if (entityOfCollection != null)
                    Rotate(entityOfCollection);
        }

        private void Rotate(IRotatable entity)
        {
            entity.Rotation = (entity.Rotation + entity.AngularAcceleration) % 360;
        }
    }
}
