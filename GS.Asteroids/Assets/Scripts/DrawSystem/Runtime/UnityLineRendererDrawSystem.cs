using GS.Asteroids.Core.Interfaces;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

namespace GS.Asteroids.DrawSystem
{
    internal sealed class UnityLineRendererDrawSystem : MonoBehaviour, IDrawSystem
    {
        private const int DefaultPoolCapacity = 1024;
        private const string ShaderName = "Unlit/Color";

        private ObjectPool<UnityLineRendererObject> _pool;
        private List<UnityLineRendererObject> _activeObjects;
        private Material _lineMaterial;

        private void Awake()
        {
            _pool = new ObjectPool<UnityLineRendererObject>
            (
                createFunc: ObjectCreate,
                actionOnGet: OnObjectGet,
                actionOnRelease: OnObjectRelease,
                defaultCapacity: DefaultPoolCapacity
            );

            _activeObjects = new List<UnityLineRendererObject>(DefaultPoolCapacity);
            _lineMaterial = new Material(Shader.Find(ShaderName));

            transform.position = Vector3.zero;
        }

        public void Draw(IEnumerable<IDrawable> drawables)
        {
            foreach (UnityLineRendererObject @object in _activeObjects)
                _pool.Release(@object);
            _activeObjects.Clear();

            foreach (IDrawable drawable in drawables)
                Draw(drawable?.GetPoints());
        }

        public void Dispose()
        {
            _pool?.Dispose();
        }

        private void Draw(Vector3[] points)
        {
            if (points == null)
                return;

            UnityLineRendererObject @object = _pool.Get();
            @object.Set(points);
            _activeObjects.Add(@object);
        }

        private UnityLineRendererObject ObjectCreate()
        {
            UnityLineRendererObject @object = new GameObject()
                .AddComponent<UnityLineRendererObject>()
                .Construct(_lineMaterial);

            @object.transform.SetParent(transform);
            @object.transform.localPosition = Vector3.zero;

            return @object;
        }

        private void OnObjectGet(UnityLineRendererObject @object)
        {
            @object.gameObject.SetActive(true);
            @object.name = "on";
        }

        private void OnObjectRelease(UnityLineRendererObject @object)
        {
            if (@object == null)
                return;

            @object.gameObject.SetActive(false);
            @object.Clear();

            @object.name = "off";
            @object.transform.SetAsFirstSibling();
        }
    }
}
