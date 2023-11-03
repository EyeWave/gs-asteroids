using GS.Asteroids.Core;
using GS.Asteroids.Core.Interfaces;
using UnityEngine;

namespace GS.Asteroids.Root
{
    internal sealed class Root : MonoBehaviour, IRoot
    {
        private RootCompositeProvider _rootCompositeProvider;

        [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.AfterSceneLoad)]
        private static void Run()
        {
            Application.targetFrameRate = 60;
            new GameObject($"[{nameof(Root)}]", typeof(Root));
        }

        private async void Awake()
        {
            _rootCompositeProvider = new RootCompositeProvider();
            Camera camera = GetConfiguratedCamera();
            IAppContext appContext = await AppContextFactory.Create(this, camera);

            new App(appContext);
        }

        public void Install<T>(T system) where T : class
        {
            _rootCompositeProvider.Install(system);
        }

        public void Uninstall<T>(T system) where T : class
        {
            _rootCompositeProvider.Uninstall(system);
        }

        private void Update()
        {
            _rootCompositeProvider?.Refresh();
        }

        private void OnDestroy()
        {
            _rootCompositeProvider?.Dispose();
        }

        private Camera GetConfiguratedCamera()
        {
            Camera camera = Camera.main;
            camera.transform.position = 10.0f * Vector3.back;
            camera.orthographic = true;
            camera.orthographicSize = 100.0f;
            camera.backgroundColor = Color.black;
            return camera;
        }
    }
}
