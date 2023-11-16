using GS.Asteroids.Core.Entity;
using GS.Asteroids.Core.Interfaces;
using System;

namespace GS.Asteroids.Core.Systems
{
    internal sealed class GameOverSystem : SystemCollectionProviderBase<IArmourer>, IRefreshable
    {
        private readonly Action _gameOverCallback;

        internal GameOverSystem(Action gameOverCallback) : base()
        {
            _gameOverCallback = gameOverCallback ?? throw new ArgumentNullException(nameof(gameOverCallback));
        }

        public void Refresh()
        {
            if (Collection.Count == 0)
                _gameOverCallback.Invoke();
        }
    }
}
