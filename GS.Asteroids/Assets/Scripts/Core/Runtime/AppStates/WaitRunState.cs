using GS.Asteroids.Core.Interfaces;
using GS.Asteroids.Core.Interfaces.AppStates;
using GS.Asteroids.Core.Interfaces.GamePlay;
using GS.Asteroids.Core.Interfaces.UIContext;
using GS.Asteroids.Core.Systems;
using System;
using System.Collections.Generic;
using System.Text;

namespace GS.Asteroids.Core.States
{
    internal sealed class WaitRunState : IAppState
    {
        public AppState State => AppState.WaitRun;

        private readonly IAppExitProvider _appExitProvider;
        private readonly IInputSystem _inputSystem;
        private readonly IUiSystem _uiSystem;
        private readonly ILocalizationSystem _localizationSystem;
        private readonly IEntityProvider _entityProvider;
        private readonly IObjectProvider _objectProvider;
        private readonly ISystemProvider _systemProvider;
        private readonly IReadOnlyCollection<ISystem> _systems;
        private readonly StringBuilder _descriptionStringBuilder;

        private bool _isMoveNext;

        public WaitRunState(
            CompositeProvider compositeProvider,
            IAppExitProvider appExitProvider,
            ILevel level,
            IAppConfigDataProvider appConfigDataProvider,
            IInputSystem inputSystem,
            IDrawSystem drawSystem,
            IUiSystem uiSystem,
            ILocalizationSystem localizationSystem)
        {
            _appExitProvider = appExitProvider;
            _inputSystem = inputSystem;
            _uiSystem = uiSystem;
            _localizationSystem = localizationSystem;
            _descriptionStringBuilder = new StringBuilder(1024);

            _entityProvider = compositeProvider;
            _objectProvider = compositeProvider;
            _systemProvider = compositeProvider;

            ICollisionCreateProvider collisionCreateProvider = compositeProvider;
            ICollisionProcessProvider collisionProcessProvider = compositeProvider;

            _systems = new List<ISystem>
            {
                new StarSystem(appConfigDataProvider, level, _entityProvider, _objectProvider),
                new MoveSystem(),

                new OutOfLevelSystem(level, collisionCreateProvider),
                new CollidablesDestroySystem(collisionProcessProvider, _entityProvider, _objectProvider),
                new RefreshClearSystem(collisionProcessProvider),

                new RefreshDrawablePointsSystem(),
                new DrawSystemProvider(drawSystem),
            };
        }

        public void Enter()
        {
            _isMoveNext = false;
            _inputSystem.AlternativeFire += DoMoveNext;
            _inputSystem.Exit += AppExit;

            foreach (ISystem system in _systems)
            {
                _systemProvider.Add(system);
                system.Init();
            }

            _descriptionStringBuilder.Append(_localizationSystem.Get(AppLocalizationKeys.PressKeyToStart).ToUpper());
            _descriptionStringBuilder.Append(Environment.NewLine);
            _descriptionStringBuilder.Append(Environment.NewLine);
            _descriptionStringBuilder.Append("<size=30>");
            _descriptionStringBuilder.Append(_localizationSystem.Get(AppLocalizationKeys.PressKeyToExit).ToUpper());
            _descriptionStringBuilder.Append("</size>");

            UiInfoContext context = new UiInfoContext
            (
                title: _localizationSystem.Get(AppLocalizationKeys.AppName).ToUpper(),
                description: _descriptionStringBuilder.ToString()
            );

            _uiSystem.ShowInfo(context);
            _descriptionStringBuilder.Clear();
        }

        public void Exit()
        {
            foreach (ISystem system in _systems)
                _systemProvider.Remove(system);

            _entityProvider.Dispose();
            _objectProvider.Dispose();
            _systemProvider.Dispose();

            _uiSystem.HideInfo();
        }

        public bool MoveNext()
        {
            return _isMoveNext;
        }

        private void DoMoveNext()
        {
            _inputSystem.AlternativeFire -= DoMoveNext;
            _inputSystem.Exit -= AppExit;
            _isMoveNext = true;
        }

        private void AppExit()
        {
            _appExitProvider.AppExit();
        }
    }
}
