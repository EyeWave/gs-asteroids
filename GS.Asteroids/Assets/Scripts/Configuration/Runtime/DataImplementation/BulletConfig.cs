using GS.Asteroids.Core.Interfaces.Configuration;
using System;
using UnityEngine;

namespace GS.Asteroids.Configuration.DataImplementation
{
    [Serializable]
    internal sealed class BulletConfig : IBulletConfig
    {
        public float Radius => _radius;
        public float Acceleration => _acceleration;

        [SerializeField, Range(0.5f, 5.0f)] private float _radius = 1.0f;
        [SerializeField, Range(1.0f, 10.0f)] private float _acceleration = 5.0f;
    }
}
