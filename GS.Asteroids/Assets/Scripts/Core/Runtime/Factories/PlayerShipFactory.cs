using GS.Asteroids.Core.Objects;

namespace GS.Asteroids.Core.Factories
{
    internal sealed class PlayerShipFactory : ObjectFactoryBase<PlayerShip>
    {
        internal override PlayerShip Create()
        {
            return new PlayerShip();
        }
    }
}
