using GS.Asteroids.Core.Interfaces;
using UnityEngine;

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

        public Vector3 GetStartPoint()
        {
            return Vector3.zero;
        }

        public override string ToString()
        {
            return $"Width: {Width} | Height: {Height} | Extents: [{ExtentWidth}x{ExtentHeight}] | Top: {Top} | Bottom: {Bottom} | Left: {Left} | Right: {Right}]";
        }
    }
}
