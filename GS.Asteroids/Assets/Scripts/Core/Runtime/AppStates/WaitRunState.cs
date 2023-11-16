using GS.Asteroids.Core.Interfaces;
using GS.Asteroids.Core.Interfaces.AppStates;
using GS.Asteroids.Core.Interfaces.GamePlay;
using GS.Asteroids.Core.Systems;
using System.Collections.Generic;

namespace GS.Asteroids.Core.States
{
    internal sealed class WaitRunState : IAppState
    {
        public AppState State => AppState.WaitRun;

        private readonly IInputSystem _inputSystem;
        private readonly IUiSystem _uiSystem;
        private readonly ILocalizationSystem _localizationSystem;
        private readonly IEntityProvider _entityProvider;
        private readonly IObjectProvider _objectProvider;
        private readonly ISystemProvider _systemProvider;
        private readonly IReadOnlyCollection<ISystem> _systems;

        private bool _isMoveNext;

        public WaitRunState(
            CompositeProvider compositeProvider,
            ILevel level,
            IAppConfigDataProvider appConfigDataProvider,
            IInputSystem inputSystem,
            IDrawSystem drawSystem,
            IUiSystem uiSystem,
            ILocalizationSystem localizationSystem)
        {
            _inputSystem = inputSystem;
            _uiSystem = uiSystem;
            _localizationSystem = localizationSystem;

            _entityProvider = compositeProvider;
            _objectProvider = compositeProvider;
            _systemProvider = compositeProvider;

            ICollisionCreateProvider collisionCreateProvider = compositeProvider;
            ICollisionProcessProvider collisionProcessProvider = compositeProvider;

            _systems = new List<ISystem>
            {
                new StarSystem(appConfigDataProvider, level, _entityProvider, _objectProvider),
                new MoveSystem(),
                new OutOfLeveSystem(level, collisionCreateProvider),
                new CollidablesDestroySystem(collisionProcessProvider, _entityProvider, _objectProvider),
                new RefreshDrawablePointsSystem(),
                new DrawSystemProvider(drawSystem),
            };
        }

        public void Enter()
        {
            _isMoveNext = false;
            _inputSystem.AlternativeFire += DoMoveNext;

            foreach (ISystem system in _systems)
            {
                _systemProvider.Add(system);
                system.Init();
            }

            _uiSystem.ShowInfo
            (
                title: _localizationSystem.Get(AppLocalizationKeys.AppName).ToUpper(),
                description: _localizationSystem.Get(AppLocalizationKeys.PressKeyToStart).ToUpper()
            );
        }

        public void Exit()
        {
            _entityProvider.Dispose();
            _objectProvider.Dispose();

            foreach (ISystem system in _systems)
                _systemProvider.Remove(system);

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
