using GS.Asteroids.Core.Interfaces;
using GS.Asteroids.Core.Interfaces.AppStates;
using GS.Asteroids.Core.Interfaces.GamePlay;
using GS.Asteroids.Core.Interfaces.UIContext;
using System;

namespace GS.Asteroids.Core.States
{
    internal sealed class GameOverSate : IAppState
    {
        public AppState State => AppState.GameOver;

        private readonly IResultProvider _resultProvider;
        private readonly IInputSystem _inputSystem;
        private readonly IUiSystem _uiSystem;
        private readonly ILocalizationSystem _localizationSystem;

        private bool _isMoveNext;

        public GameOverSate(
            IResultProvider resultProvider,
            IInputSystem inputSystem,
            IUiSystem uiSystem,
            ILocalizationSystem localizationSystem)
        {
            _resultProvider = resultProvider ?? throw new ArgumentNullException(nameof(resultProvider));
            _inputSystem = inputSystem ?? throw new ArgumentNullException(nameof(inputSystem));
            _uiSystem = uiSystem ?? throw new ArgumentNullException(nameof(uiSystem));
            _localizationSystem = localizationSystem ?? throw new ArgumentNullException(nameof(localizationSystem));
        }

        public void Enter()
        {
            _isMoveNext = false;
            _inputSystem.AlternativeFire += DoMoveNext;

            UiInfoContext context = new UiInfoContext
            (
                title: _localizationSystem.Get(AppLocalizationKeys.GameOver).ToUpper(),
                description: $"YOU RESULT: {_resultProvider.Result}{Environment.NewLine}{Environment.NewLine}<size=30>{_localizationSystem.Get(AppLocalizationKeys.PressKeyToContinue).ToLower()}</size>"
            );

            _uiSystem.ShowInfo(context);
        }

        public void Exit()
        {
            _uiSystem.HideInfo();
            _resultProvider.Clear();
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
