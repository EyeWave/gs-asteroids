using System;
using System.Collections.Generic;
using System.Linq;
using Mathf = UnityEngine.Mathf;
using Random = UnityEngine.Random;
using Vector3 = UnityEngine.Vector3;

namespace GS.Asteroids.Configuration
{
    internal sealed class AsteroidsCorePointsGenerator
    {
        private readonly Dictionary<float, IReadOnlyList<Vector3>> _billetCacheMap = new Dictionary<float, IReadOnlyList<Vector3>>(8);

        internal IReadOnlyList<Vector3> GetCorePointsOfPlayerShip(float radius)
        {
            return new Vector3[]
            {
                new Vector3(-radius, -radius),
                new Vector3(0.0f, radius),
                new Vector3(radius, -radius),
            };
        }

        internal IReadOnlyList<Vector3> GetCorePointsOfAsteroid(float radius)
        {
            float maxNoise = radius * 0.5f;
            int numOfPoints = Mathf.Max(3, Mathf.RoundToInt(maxNoise));
            Func<float> getNoise = () => Random.Range(-maxNoise, 0.0f);

            return GetRoundPoints(radius, numOfPoints, getNoise);
        }

        internal IReadOnlyList<Vector3> GetCorePointsOfBullet(float radius)
        {
            if (!_billetCacheMap.TryGetValue(radius, out IReadOnlyList<Vector3> result))
            {
                if (radius < 2.0f)
                {
                    result = new Vector3[]
                    {
                        radius * Vector3.up,
                        radius * Vector3.down,
                    };
                }
                else
                {
                    result = GetRoundPoints(radius, numOfPoints: 6, getNoise: () => 0.0f);
                }

                _billetCacheMap.Add(radius, result);
            }

            return result;
        }

        private IReadOnlyList<Vector3> GetRoundPoints(float radius, int numOfPoints, Func<float> getNoise)
        {
            return Enumerable
                .Range(0, numOfPoints)
                .Select(index =>
                {
                    float angle = Map(index, 0, numOfPoints, 0, 2 * Mathf.PI);
                    float resultRadius = radius + getNoise.Invoke();
                    return new Vector3(resultRadius * Mathf.Cos(angle), resultRadius * Mathf.Sin(angle));
                })
                .ToArray();
        }

        private float Map(float value, float fromRangeStart, float fromRangeEnd, float toRangeStart, float toRangeEnd)
        {
            return toRangeStart + (value - fromRangeStart) * (toRangeEnd - toRangeStart) / (fromRangeEnd - fromRangeStart);
        }
    }
}
