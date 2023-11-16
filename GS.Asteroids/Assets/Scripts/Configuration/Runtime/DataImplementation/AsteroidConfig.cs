using GS.Asteroids.Core.Interfaces.Configuration;
using System;
using UnityEngine;

namespace GS.Asteroids.Configuration.DataImplementation
{
    [Serializable]
    internal sealed class AsteroidConfig : IAsteroidConfig
    {
        public float RadiusMin => _radiusMin;
        public float RadiusMax => _radiusMax;
        public float AccelerationMin => _accelerationMin;
        public float AccelerationMax => _accelerationMax;
        public int CountMin => _countMin;
        public int CountUp => _countUp;
        public float SpawnIntervalSec => _spawnIntervalSec;
        public float UpIntervalSec => _upIntervalSec;

        [SerializeField, Range(5.0f, 50.0f)] private float _radiusMin = 10.0f;
        [SerializeField, Range(5.0f, 50.0f)] private float _radiusMax = 20.0f;
        [SerializeField, Range(0.1f, 5.0f)] private float _accelerationMin = 0.25f;
        [SerializeField, Range(0.1f, 5.0f)] private float _accelerationMax = 1.0f;
        [SerializeField, Range(0, 30)] private int _countMin = 1;
        [SerializeField, Range(0, 30)] private int _countUp = 1;
        [SerializeField, Range(0.1f, 30.0f)] private float _spawnIntervalSec = 1.0f;
        [SerializeField, Range(0.1f, 30.0f)] private float _upIntervalSec = 2.0f;
    }
}
