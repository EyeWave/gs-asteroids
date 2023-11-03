using GS.Asteroids.Core.Interfaces.Configuration;
using System;
using UnityEngine;

namespace GS.Asteroids.Configuration.DataImplementation
{
    [Serializable]
    internal sealed class PlayerConfig : IPlayerConfig
    {
        public float Radius => _radius;
        public float MaxAcceleration => _maxAcceleration;
        public float InertionMultipler => _inertionMultipler;

        [SerializeField, Range(2.0f, 10.0f)] private float _radius = 3.0f;
        [SerializeField, Range(0.5f, 10.0f)] private float _maxAcceleration = 2.0f;
        [SerializeField, Range(0.001f, 1.0f)] private float _inertionMultipler = 0.05f;
    }
}
