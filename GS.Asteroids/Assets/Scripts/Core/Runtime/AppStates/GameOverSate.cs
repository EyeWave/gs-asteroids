using GS.Asteroids.Core.Interfaces;
using GS.Asteroids.Core.Interfaces.AppStates;

namespace GS.Asteroids.Core.States
{
    internal sealed class GameOverSate : IAppState
    {
        private readonly IDebugLogger _logger;

        public AppState State => AppState.GameOver;

        public GameOverSate(IDebugLogger logger)
        {
            _logger = logger;
        }

        public void Enter()
        {
            _logger.Log("--- GAME OVER ---");
        }

        public void Exit()
        {
        }

        public bool MoveNext()
        {
            return false;
        }
    }
}
