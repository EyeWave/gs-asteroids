using GS.Asteroids.Core.Entity;

namespace GS.Asteroids.Core.Factories
{
    internal class ObjectFactory<T> where T : class, IEntity, new()
    {
        public T Create()
        {
            return new T();
        }
    }
}
