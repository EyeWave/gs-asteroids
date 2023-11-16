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
        public int CountMax => _countMax;
        public float SpawnIntervalSec => _spawnIntervalSec;

        [SerializeField, Range(3.0f, 50.0f)] private float _radius = 5.0f;
        [SerializeField, Range(0.1f, 5.0f)] private float _accelerationMin = 0.25f;
        [SerializeField, Range(0.1f, 5.0f)] private float _accelerationMax = 1.0f;
        [SerializeField, Range(0, 30)] private int _countMax = 1;
        [SerializeField, Range(0.1f, 30.0f)] private float _spawnIntervalSec = 1.0f;
    }
}
