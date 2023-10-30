using GS.Asteroids.Core.Interfaces;
using UnityEngine;

namespace GS.Asteroids.Level
{
    public static class AsteroidsLevelFactory
    {
        public static ILevel Create(Camera camera)
        {
            return new AsteroidsLevel
            (
                viewAspect: camera.aspect,
                viewOrthographicSize: camera.orthographicSize
            );
        }
    }
}
