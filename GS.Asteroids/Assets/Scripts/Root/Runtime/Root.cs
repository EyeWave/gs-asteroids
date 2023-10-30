using GS.Asteroids.Core;
using GS.Asteroids.Core.Interfaces;
using GS.Asteroids.DrawSystem;
using GS.Asteroids.InputSystem;
using GS.Asteroids.Level;
using GS.Asteroids.Logger;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace GS.Asteroids.Root
{
    [RequireComponent(typeof(Camera))]
    internal sealed class Root : MonoBehaviour, IRoot
    {
        private RootCompositeProvider _rootCompositeProvider;

        private List<Vector3> _vertices;

        private void Awake()
        {
            Application.targetFrameRate = 60;

            Camera camera = GetConfiguratedCamera();
            _rootCompositeProvider = new RootCompositeProvider();

            IDebugLogger logger = AsteroidsLoggerFactory.Create();
            IInputSystem inputSystem = AsteroidsInputSystemFactory.Create();
            IDrawSystem drawSystem = AsteroidsDrawSystemFactory.Create();
            ILevel level = AsteroidsLevelFactory.Create(camera);

            Game game = new Game(this, level, drawSystem, inputSystem, logger);

            _vertices = GetVertices();
            CreateSpheres(level);
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

        private Camera GetConfiguratedCamera()
        {
            Camera camera = GetComponent<Camera>();
            camera.transform.position = 10.0f * Vector3.back;
            camera.orthographic = true;
            camera.orthographicSize = 100.0f;
            return camera;
        }

        private void CreateSpheres(ILevel level)
        {
            GameObject sphere;

            sphere = GameObject.CreatePrimitive(PrimitiveType.Sphere);
            sphere.transform.position = new Vector3(level.Left, level.Top);
            sphere.name = "Left-Top";

            sphere = GameObject.CreatePrimitive(PrimitiveType.Sphere);
            sphere.transform.position = new Vector3(level.Left, level.Bottom);
            sphere.name = "Left-Bottom";

            sphere = GameObject.CreatePrimitive(PrimitiveType.Sphere);
            sphere.transform.position = new Vector3(level.Right, level.Top);
            sphere.name = "Right-Top";

            sphere = GameObject.CreatePrimitive(PrimitiveType.Sphere);
            sphere.transform.position = new Vector3(level.Right, level.Bottom);
            sphere.name = "Right-Bottom";

            //sphere = GameObject.CreatePrimitive(PrimitiveType.Sphere);
            //sphere.transform.position = Vector3.zero;
            //sphere.name = "Center";
        }

        private List<Vector3> GetVertices()
        {
            int num = 10;
            float radius = 20.0f;

            return Enumerable
                .Range(0, num)
                .Select(i =>
                {
                    float angle = Map(i, 0, num, 0, 2 * Mathf.PI);
                    float r = radius + Random.Range(-10.0f, 0.0f);
                    return new Vector3(r * Mathf.Cos(angle), r * Mathf.Sin(angle));
                })
                .ToList();
        }

        private float Map(float value, float fromRangeStart, float fromRangeEnd, float toRangeStart, float toRangeEnd)
        {
            return toRangeStart + (value - fromRangeStart) * (toRangeEnd - toRangeStart) / (fromRangeEnd - fromRangeStart);
        }

        private void OnDrawGizmos()
        {
            if (_vertices == null)
                return;

            Gizmos.color = Color.cyan;
            foreach (Vector3 vertice in _vertices)
                Gizmos.DrawSphere(vertice, 1.0f);
        }
    }
}
