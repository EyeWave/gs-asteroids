using GS.Asteroids.Core.Interfaces;
using GS.Asteroids.Core.Interfaces.AppStates;
using System;

namespace GS.Asteroids.Core.States
{
    internal sealed class GameOverSate : IAppState
    {
        public AppState State => AppState.GameOver;

        private readonly IInputSystem _inputSystem;
        private readonly IUiSystem _uiSystem;
        private readonly ILocalizationSystem _localizationSystem;

        private bool _isMoveNext;

        public GameOverSate(
            IInputSystem inputSystem,
            IUiSystem uiSystem,
            ILocalizationSystem localizationSystem)
        {
            _inputSystem = inputSystem ?? throw new ArgumentNullException(nameof(inputSystem));
            _uiSystem = uiSystem ?? throw new ArgumentNullException(nameof(uiSystem));
            _localizationSystem = localizationSystem ?? throw new ArgumentNullException(nameof(localizationSystem));
        }

        public void Enter()
        {
            _isMoveNext = false;
            _inputSystem.AlternativeFire += DoMoveNext;

            _uiSystem.ShowInfo
            (
                title: _localizationSystem.Get(AppLocalizationKeys.GameOver).ToUpper(),
                description: $"YOU RESULT: 0{Environment.NewLine}{Environment.NewLine}<size=30>{_localizationSystem.Get(AppLocalizationKeys.PressKeyToContinue).ToLower()}</size>"
            );
        }

        public void Exit()
        {
            _uiSystem.HideInfo();
        }

        public bool MoveNext()
        {
            return _isMoveNext;
        }

        private void DoMoveNext()
        {
            _inputSystem.AlternativeFire -= DoMoveNext;
            _isMoveNext = true;
        }
    }
}
