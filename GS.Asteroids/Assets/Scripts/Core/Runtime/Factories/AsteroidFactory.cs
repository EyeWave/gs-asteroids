using GS.Asteroids.Core.Objects;

namespace GS.Asteroids.Core.Factories
{
    internal sealed class AsteroidFactory : ObjectFactoryBase<Asteroid>
    {
        internal override Asteroid Create()
        {
            return new Asteroid();
        }
    }
}
