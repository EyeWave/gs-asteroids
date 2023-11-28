using GS.Asteroids.Core.Interfaces;
using GS.Asteroids.Core.Interfaces.AppStates;
using GS.Asteroids.Core.Interfaces.GamePlay;
using GS.Asteroids.Core.Interfaces.UIContext;
using System;
using System.Text;

namespace GS.Asteroids.Core.States
{
    internal sealed class GameOverSate : IAppState
    {
        public AppState State => AppState.GameOver;

        private readonly IResultProvider _resultProvider;
        private readonly IInputSystem _inputSystem;
        private readonly IUiSystem _uiSystem;
        private readonly ILocalizationSystem _localizationSystem;
        private readonly StringBuilder _descriptionStringBuilder;

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
            _descriptionStringBuilder = new StringBuilder(1024);
        }

        public void Enter()
        {
            _isMoveNext = false;
            _inputSystem.AlternativeFire += DoMoveNext;

            _descriptionStringBuilder.Append(_localizationSystem.Get(AppLocalizationKeys.YouResult).ToUpper());
            _descriptionStringBuilder.Append(": ");
            _descriptionStringBuilder.Append(_resultProvider.Result);
            _descriptionStringBuilder.Append(Environment.NewLine);
            _descriptionStringBuilder.Append(Environment.NewLine);
            _descriptionStringBuilder.Append("<size=30>");
            _descriptionStringBuilder.Append(_localizationSystem.Get(AppLocalizationKeys.PressKeyToContinue).ToLower());
            _descriptionStringBuilder.Append("</size>");

            UiInfoContext context = new UiInfoContext
            (
                title: _localizationSystem.Get(AppLocalizationKeys.GameOver).ToUpper(),
                description: _descriptionStringBuilder.ToString()
            );

            _uiSystem.ShowInfo(context);
            _descriptionStringBuilder.Clear();
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
