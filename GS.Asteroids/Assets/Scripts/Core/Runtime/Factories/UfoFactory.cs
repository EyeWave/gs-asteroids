using GS.Asteroids.Core.Objects;

namespace GS.Asteroids.Core.Factories
{
    internal sealed class UfoFactory : ObjectFactoryBase<Ufo>
    {
        internal override Ufo Create()
        {
            return new Ufo();
        }
    }
}
