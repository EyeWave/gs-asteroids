using GS.Asteroids.Core.Interfaces;
using System;
using System.Collections.Generic;

namespace GS.Asteroids.Root
{
    internal sealed class RootCompositeProvider : IRoot, IRefreshable, IDisposable
    {
        private readonly List<IRefreshable> _refreshables = new List<IRefreshable>(1024);
        private readonly List<IDisposable> _disposables = new List<IDisposable>(1024);

        private readonly HashSet<IRefreshable> _refreshablesInstall = new HashSet<IRefreshable>(1024);
        private readonly HashSet<IDisposable> _disposablesInstall = new HashSet<IDisposable>(1024);

        private readonly HashSet<IRefreshable> _refreshablesUninstall = new HashSet<IRefreshable>(1024);
        private readonly HashSet<IDisposable> _disposablesUninstall = new HashSet<IDisposable>(1024);

        public void Install<T>(T system) where T : class
        {
            if (system is IRefreshable refreshable)
                _refreshablesInstall.Add(refreshable);

            if (system is IDisposable disposable)
                _disposablesInstall.Add(disposable);
        }

        public void Uninstall<T>(T system) where T : class
        {
            if (system is IRefreshable refreshable)
                _refreshablesUninstall.Add(refreshable);

            if (system is IDisposable disposable)
                _disposablesUninstall.Add(disposable);
        }

        public void Refresh()
        {
            foreach (IRefreshable refreshable in _refreshables)
            {
                if (refreshable != null && !_refreshablesUninstall.Contains(refreshable))
                    refreshable.Refresh();
            }

            if (_refreshablesInstall.Count > 0)
            {
                foreach (IRefreshable refreshable in _refreshablesInstall)
                {
                    _refreshables.Add(refreshable);
                    refreshable.Refresh();
                }

                _refreshablesInstall.Clear();
            }

            if (_disposablesInstall.Count > 0)
            {
                foreach (IDisposable disposable in _disposablesInstall)
                    _disposables.Add(disposable);

                _disposablesInstall.Clear();
            }

            if (_refreshablesUninstall.Count > 0)
            {
                foreach (IRefreshable refreshable in _refreshablesUninstall)
                    _refreshables.Remove(refreshable);

                _refreshablesUninstall.Clear();
            }

            if (_disposablesUninstall.Count > 0)
            {
                foreach (IDisposable disposable in _disposablesUninstall)
                {
                    _disposables.Remove(disposable);
                    disposable.Dispose();
                }

                _disposablesUninstall.Clear();
            }
        }

        public void Dispose()
        {
            foreach (IDisposable disposable in _disposables)
                disposable?.Dispose();

            foreach (IDisposable disposable in _disposablesInstall)
                disposable?.Dispose();

            foreach (IDisposable disposable in _disposablesUninstall)
                disposable?.Dispose();

            _refreshables.Clear();
            _refreshablesInstall.Clear();
            _refreshablesUninstall.Clear();

            _disposables.Clear();
            _disposablesInstall.Clear();
            _disposablesUninstall.Clear();
        }
    }
}
