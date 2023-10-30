using System;
using UnityEngine;

namespace GS.Asteroids.DrawSystem
{
    [RequireComponent(typeof(LineRenderer))]
    internal sealed class UnityLineRendererObject : MonoBehaviour
    {
        private static Vector3[] EmptyPoints = new Vector3[0];

        private LineRenderer _lineRenderer;

        internal UnityLineRendererObject Construct(Material material)
        {
            _lineRenderer = GetComponent<LineRenderer>();
            _lineRenderer.material = material;
            return this;
        }

        internal void Set(Vector3[] points, bool isLoop = true)
        {
            if (_lineRenderer == null)
                throw new TypeInitializationException(nameof(UnityLineRendererObject), new Exception("Not Initialized"));

            _lineRenderer.positionCount = points.Length;
            _lineRenderer.loop = isLoop;
            _lineRenderer.SetPositions(points);
        }

        internal void Clear()
        {
            _lineRenderer.positionCount = 0;
            _lineRenderer.SetPositions(EmptyPoints);
        }
    }
}
