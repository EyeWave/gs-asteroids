using GS.Asteroids.Core.Interfaces;
using GS.Asteroids.Core.Interfaces.GamePlay;
using System;

namespace GS.Asteroids.Core.Systems
{
    internal sealed class RefreshClearSystem : ISystem, IRefreshable
    {
        private readonly ICollisionProcessProvider _collisionProcessProvider;

        internal RefreshClearSystem(ICollisionProcessProvider collisionProcessProvider)
        {
            _collisionProcessProvider = collisionProcessProvider ?? throw new ArgumentNullException(nameof(collisionProcessProvider));
        }

        public void Refresh()
        {
            _collisionProcessProvider.Clear();
        }
    }
}
