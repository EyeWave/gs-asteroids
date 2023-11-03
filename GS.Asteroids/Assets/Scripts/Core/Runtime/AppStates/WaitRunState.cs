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
        private readonly IDebugLogger _logger;

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
            IDebugLogger logger)
        {
            _inputSystem = inputSystem;
            _logger = logger;

            _entityProvider = compositeProvider;
            _objectProvider = compositeProvider;
            _systemProvider = compositeProvider;

            ICollisionCreateProvider collisionCreateProvider = compositeProvider;
            ICollisionProcessProvider collisionProcessProvider = compositeProvider;

            StarSystem starSystem = new StarSystem(appConfigDataProvider, level, _entityProvider, _objectProvider);
            MoveSystem moveSystem = new MoveSystem();
            OutOfLeveSystem outOfLeveSystem = new OutOfLeveSystem(level, collisionCreateProvider);
            CollidablesDestroySystem collidablesDestroySystem = new CollidablesDestroySystem(collisionProcessProvider, _entityProvider, _objectProvider);
            RefreshDrawablePointsSystem refreshDrawablePointsSystem = new RefreshDrawablePointsSystem();
            DrawSystemProvider drawSystemProvider = new DrawSystemProvider(drawSystem);

            _systems = new List<ISystem>
            {
                starSystem,
                moveSystem,
                outOfLeveSystem,
                collidablesDestroySystem,
                refreshDrawablePointsSystem,
                drawSystemProvider,
            };
        }

        public void Enter()
        {
            _isMoveNext = false;
            _inputSystem.Fire += DoMoveNext;

            foreach (ISystem system in _systems)
            {
                _systemProvider.Add(system);
                system.Init();
            }

            _logger.Log("PRESS SPASE TO START");
        }

        public void Exit()
        {
            _entityProvider.Dispose();
            _objectProvider.Dispose();

            foreach (ISystem system in _systems)
                _systemProvider.Remove(system);
        }

        public bool MoveNext()
        {
            return _isMoveNext;
        }

        private void DoMoveNext()
        {
            _inputSystem.Fire -= DoMoveNext;
            _isMoveNext = true;
        }
    }
}
