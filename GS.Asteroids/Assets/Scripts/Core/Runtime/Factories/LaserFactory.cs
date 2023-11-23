using GS.Asteroids.Core.Objects;

namespace GS.Asteroids.Core.Factories
{
    internal sealed class LaserFactory : ObjectFactoryBase<Laser>
    {
        internal override Laser Create()
        {
            return new Laser();
        }
    }
}
