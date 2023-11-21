using GS.Asteroids.Core.Interfaces.Configuration;
using System;
using UnityEngine;

namespace GS.Asteroids.Configuration.DataImplementation
{
    [Serializable]
    internal sealed class ChipConfig : IChipConfig
    {
        public int QuantityOnDestroyOfAsteroid => _quantityOnDestroyOfAsteroid;
        public float MultiplierOfAsteroidRadiusMin => _multiplierOfAsteroidRadiusMin;
        public float MultiplierOfAsteroidAccelerationMax => _multiplierOfAsteroidAccelerationMax;
        public int Reward => _reward;

        [SerializeField, Range(1, 10)] private int _quantityOnDestroyOfAsteroid = 2;
        [SerializeField, Range(0.1f, 1.0f)] private float _multiplierOfAsteroidRadiusMin = 0.3f;
        [SerializeField, Range(1.1f, 5.0f)] private float _multiplierOfAsteroidAccelerationMax = 1.5f;
        [SerializeField, Range(1, 30)] private int _reward = 2;
    }
}
