using GS.Asteroids.Core.Entity;

namespace GS.Asteroids.Core.Factories
{
    internal abstract class ObjectProviderBase<T> : IObjectProvider<T> where T : class, IEntity
    {
        private readonly ObjectPool<T> _objectPool;

        protected ObjectProviderBase(ObjectFactoryBase<T> objectFactory)
        {
            _objectPool = new ObjectPool<T>(objectFactory.Create);
        }

        public T Take()
        {
            T @object = _objectPool.Take();
            OnTake(@object);
            return @object;
        }

        public void Return(IEntity @object)
        {
            if (@object is T objectT)
            {
                OnReturn(objectT);
                _objectPool.Return(objectT);
            }
            else
            {
                throw new System.ArgumentException($"Type '{@object.GetType().Name}' does not match provider type '{typeof(T).Name}'");
            }
        }

        protected abstract void OnTake(T @object);

        protected abstract void OnReturn(T @object);
    }
}
