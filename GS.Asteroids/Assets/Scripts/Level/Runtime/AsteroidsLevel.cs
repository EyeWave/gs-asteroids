using GS.Asteroids.Core.Interfaces;
using System;
using Random = UnityEngine.Random;
using Vector3 = UnityEngine.Vector3;

namespace GS.Asteroids.Level
{
    internal sealed class AsteroidsLevel : ILevel
    {
        public float ExtentWidth { get; }
        public float ExtentHeight { get; }
        public float Width { get; }
        public float Height { get; }

        public float Top { get; }
        public float Bottom { get; }
        public float Left { get; }
        public float Right { get; }

        internal AsteroidsLevel(float viewAspect, float viewOrthographicSize)
        {
            ExtentWidth = viewAspect * viewOrthographicSize;
            ExtentHeight = viewOrthographicSize;

            Width = ExtentWidth * 2.0f;
            Height = ExtentHeight * 2.0f;

            Top = ExtentHeight;
            Bottom = -ExtentHeight;
            Left = -ExtentWidth;
            Right = ExtentWidth;
        }

        public Vector3 GetPlayerStartPosition()
        {
            return Vector3.zero;
        }

        public Vector3 GetEnemyStartPosition(float radius)
        {
            return Random.Range(0, 4) switch
            {
                0 => new Vector3(Random.Range(Left - radius, Right + radius), Top + radius), // top
                1 => new Vector3(Random.Range(Left - radius, Right + radius), Bottom - radius), // bottom
                2 => new Vector3(Left - radius, Random.Range(Bottom - radius, Top + radius)), // left
                3 => new Vector3(Right + radius, Random.Range(Bottom - radius, Top + radius)), // right
                _ => throw new ArgumentOutOfRangeException("The level has only 4 sides"),
            };
        }

        public override string ToString()
        {
            return $"Width: {Width} | Height: {Height} | Extents: [{ExtentWidth}x{ExtentHeight}] | Top: {Top} | Bottom: {Bottom} | Left: {Left} | Right: {Right}]";
        }
    }
}
