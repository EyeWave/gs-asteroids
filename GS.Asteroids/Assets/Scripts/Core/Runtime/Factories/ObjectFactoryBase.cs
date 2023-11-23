using GS.Asteroids.Core.Entity;

namespace GS.Asteroids.Core.Factories
{
    internal abstract class ObjectFactoryBase<T> where T : IEntity
    {
        internal abstract T Create();
    }
}
