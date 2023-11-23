using GS.Asteroids.Core.Objects;

namespace GS.Asteroids.Core.Factories
{
    internal sealed class BulletFactory : ObjectFactoryBase<Bullet>
    {
        internal override Bullet Create()
        {
            return new Bullet();
        }
    }
}
