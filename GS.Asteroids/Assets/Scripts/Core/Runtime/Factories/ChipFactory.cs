using GS.Asteroids.Core.Objects;

namespace GS.Asteroids.Core.Factories
{
    internal sealed class ChipFactory : ObjectFactoryBase<Chip>
    {
        internal override Chip Create()
        {
            return new Chip();
        }
    }
}
