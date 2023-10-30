using GS.Asteroids.Core.Entity;
using System.Collections.Generic;
using UnityEngine;

namespace GS.Asteroids.Core.Player
{
    internal class PlayerShip : IDrawableProvider, ITeleportable
    {
        public Vector3 Position { get; set; } = Vector3.zero;
        public Vector3 Velocity { get; set; } = Vector3.zero;
        public float Acceleration { get; set; } = 0.0f;

        public float Rotation { get; set; } = 0.0f;
        public float AngularAcceleration { get; set; } = 0.0f;

        public float Radius => _drawablePointsContainer.Radius;

        private readonly EntityDrawablePointsContainer _drawablePointsContainer;

        public PlayerShip(Vector3 position, EntityDrawablePointsContainer drawablePointsContainer)
        {
            Position = position;
            Rotation = 0.0f;

            _drawablePointsContainer = drawablePointsContainer;
        }

        public Vector3[] GetPoints()
        {
            return _drawablePointsContainer.Points;
        }

        public IReadOnlyList<Vector3> GetCorePoints()
        {
            return _drawablePointsContainer.CorePoints;
        }
    }
}
