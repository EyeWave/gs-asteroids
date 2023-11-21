using GS.Asteroids.Core.Interfaces.Configuration;
using System;
using UnityEngine;

namespace GS.Asteroids.Configuration.DataImplementation
{
    [Serializable]
    internal sealed class UfoConfig : IUfoConfig
    {
        public float Radius => _radius;
        public float AccelerationMin => _accelerationMin;
        public float AccelerationMax => _accelerationMax;
        public float InertionMultipler => _inertionMultipler;
        public int CountMax => _countMax;
        public float SpawnIntervalSec => _spawnIntervalSec;
        public int Reward => _reward;

        [SerializeField, Range(3.0f, 50.0f)] private float _radius = 5.0f;
        [SerializeField, Range(0.1f, 5.0f)] private float _accelerationMin = 0.75f;
        [SerializeField, Range(0.1f, 5.0f)] private float _accelerationMax = 1.25f;
        [SerializeField, Range(0.001f, 1.0f)] private float _inertionMultipler = 0.025f;
        [SerializeField, Range(0, 30)] private int _countMax = 2;
        [SerializeField, Range(0.1f, 30.0f)] private float _spawnIntervalSec = 10f;
        [SerializeField, Range(1, 30)] private int _reward = 3;
    }
}
