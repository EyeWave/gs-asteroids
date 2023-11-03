using GS.Asteroids.Core.Entity;

namespace GS.Asteroids.Core.Interfaces.GamePlay
{
    internal interface ICollisionCreateProvider
    {
        void Create(ICollidable first);

        void Create(ICollidable first, ICollidable second);
    }
}
