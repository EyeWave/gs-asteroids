using GS.Asteroids.Core;
using GS.Asteroids.Core.Interfaces;
using GS.Asteroids.InputSystem;
using GS.Asteroids.Logger;
using UnityEngine;

namespace GS.Asteroids.Root
{
    [RequireComponent(typeof(Camera))]
    public sealed class Root : MonoBehaviour, IRoot
    {
        private RootCompositeProvider _rootCompositeProvider;

        private void Awake()
        {
            Application.targetFrameRate = 60;

            IInputSystem inputSystem = AsteroidsInputSystemFactory.Create();
            IDebugLogger logger = AsteroidsLoggerFactory.Create();

            _rootCompositeProvider = new RootCompositeProvider();
            _rootCompositeProvider.Install(inputSystem);
            _rootCompositeProvider.Install(logger);

            Game game = new Game(this, inputSystem, logger);

            _rootCompositeProvider.Install(game);
        }

        public void Install<T>(T system) where T : class
        {
            _rootCompositeProvider.Install(system);
        }

        private void Update()
        {
            _rootCompositeProvider?.Refresh();
        }

        private void OnDestroy()
        {
            _rootCompositeProvider?.Dispose();
        }
    }
}
