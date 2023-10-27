using GS.Asteroids.Core.Interfaces;
using System;

namespace GS.Asteroids.Core
{
    public class Game : IRefreshable, IDisposable
    {
        private readonly IRoot _root;
        private readonly IInputSystem _inputSystem;
        private readonly IDebugLogger _logger;

        public Game(
            IRoot root,
            IInputSystem inputSystem,
            IDebugLogger logger)
        {
            _root = root ?? throw new ArgumentNullException(nameof(root));
            _inputSystem = inputSystem ?? throw new ArgumentNullException(nameof(inputSystem));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));

            _inputSystem.Fire += OnFire;
            _inputSystem.AlternativeFire += OnAlternativeFire;
        }

        public void Dispose()
        {
            _inputSystem.Fire -= OnFire;
            _inputSystem.AlternativeFire -= OnAlternativeFire;
        }

        public void Refresh()
        {
            _logger.Log($"input = {_inputSystem.GetMove()}");
        }

        private void OnFire()
        {
            _logger.Log($"<color=green>OnFire</color>");
        }

        private void OnAlternativeFire()
        {
            _logger.Log($"<color=red>OnAlternativeFire</color>");
        }
    }
}
