using GS.Asteroids.Core.Interfaces;
using System;
using System.Collections.Generic;

namespace GS.Asteroids.Root
{
    public sealed class RootCompositeProvider : IRoot, IRefreshable, IDisposable
    {
        private readonly HashSet<IRefreshable> _refreshables = new HashSet<IRefreshable>(128);
        private readonly HashSet<IDisposable> _disposables = new HashSet<IDisposable>(128);

        public void Install<T>(T system) where T : class
        {
            if (system is IRefreshable refreshable)
                _refreshables.Add(refreshable);

            if (system is IDisposable disposable)
                _disposables.Add(disposable);
        }

        public void Refresh()
        {
            foreach (IRefreshable refreshable in _refreshables)
                refreshable?.Refresh();
        }

        public void Dispose()
        {
            foreach (IDisposable disposable in _disposables)
                disposable?.Dispose();
        }
    }
}
