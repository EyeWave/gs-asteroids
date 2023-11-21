using GS.Asteroids.Core.Interfaces.Configuration;
using System;
using UnityEngine;

namespace GS.Asteroids.Configuration.DataImplementation
{
    [Serializable]
    internal sealed class LaserConfig : ILaserConfig
    {
        public float Radius => _radius;
        public float Acceleration => _acceleration;
        public int CountMax => _countMax;
        public float CoolDownIntervalSec => _coolDownIntervalSec;

        [SerializeField, Range(1.0f, 5.0f)] private float _radius = 1.0f;
        [SerializeField, Range(1.0f, 10.0f)] private float _acceleration = 4.0f;
        [SerializeField, Range(1, 50)] private int _countMax = 3;
        [SerializeField, Range(1.0f, 20.0f)] private float _coolDownIntervalSec = 5.0f;
    }
}
