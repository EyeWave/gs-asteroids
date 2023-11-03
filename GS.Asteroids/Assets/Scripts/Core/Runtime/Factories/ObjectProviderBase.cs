using GS.Asteroids.Core.Entity;

namespace GS.Asteroids.Core.Factories
{
    internal abstract class ObjectProviderBase<T> where T : class, IEntity, new()
    {
        private readonly ObjectPool<T> _objectPool;

        protected ObjectProviderBase(ObjectFactory<T> objectFactory)
        {
            _objectPool = new ObjectPool<T>(objectFactory.Create);
        }

        public T Take()
        {
            T @object = _objectPool.Take();
            OnTake(@object);
            return @object;
        }

        public void Return(T @object)
        {
            OnReturn(@object);
            _objectPool.Return(@object);
        }

        protected abstract void OnTake(T @object);

        protected abstract void OnReturn(T @object);
    }
}
