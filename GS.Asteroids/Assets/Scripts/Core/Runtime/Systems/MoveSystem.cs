using GS.Asteroids.Core.Entity;
using GS.Asteroids.Core.Interfaces;

namespace GS.Asteroids.Core.Systems
{
    internal sealed class MoveSystem : SystemCollectionProviderBase<IMovable>, IRefreshable
    {
        internal MoveSystem() : base()
        {
        }

        public void Refresh()
        {
            foreach (IMovable entityOfCollection in Collection)
                if (entityOfCollection != null)
                    Move(entityOfCollection);
        }

        private void Move(IMovable entity)
        {
            entity.Position += entity.Velocity;
        }
    }
}
