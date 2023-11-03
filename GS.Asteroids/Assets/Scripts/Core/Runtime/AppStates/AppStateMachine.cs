using GS.Asteroids.Core.Interfaces;
using GS.Asteroids.Core.Interfaces.AppStates;
using System;
using System.Collections.Generic;

namespace GS.Asteroids.Core.States
{
    internal sealed class AppStateMachine : IRefreshable, IDisposable
    {
        private readonly Func<AppState, AppState> _stateSwitcher;
        private readonly IDebugLogger _logger;
        private readonly IDictionary<AppState, IAppState> _stateMap;

        private IAppState _currentState;

        internal AppStateMachine(Func<AppState, AppState> stateSwitcher, IDebugLogger logger)
        {
            _stateSwitcher = stateSwitcher ?? throw new ArgumentNullException(nameof(stateSwitcher));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _stateMap = new Dictionary<AppState, IAppState>(8);
        }

        internal void SetStates(params IAppState[] appStates)
        {
            foreach (IAppState appState in appStates)
            {
                _stateMap.Add(appState.State, appState);

                if (_currentState == null)
                {
                    _currentState = appState;
                    _currentState.Enter();
                }
            }
        }

        public void Refresh()
        {
            if (_currentState == null)
                return;

            if (_currentState.MoveNext())
            {
                AppState state = _stateSwitcher.Invoke(_currentState.State);
                if (!_stateMap.TryGetValue(state, out IAppState appState))
                {
                    _logger.LogError($"Unknown or not installed state {state}");
                    appState = _currentState;
                }

                _currentState.Exit();
                _currentState = appState;
                _currentState.Enter();
            }
        }

        public void Dispose()
        {
            _currentState = null;
            _stateMap.Clear();
        }
    }
}
