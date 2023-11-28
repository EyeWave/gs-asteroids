using GS.Asteroids.Core.Entity;
using System;

namespace GS.Asteroids.Core.Providers
{
    internal abstract class ObjectProviderBase<T> : IObjectProvider<T> where T : class, IEntity
    {
        private readonly ObjectPool<T> _objectPool;

        protected ObjectProviderBase(Func<T> objectGenerator)
        {
            _objectPool = new ObjectPool<T>(objectGenerator);
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
                throw new ArgumentException($"Type '{@object.GetType().Name}' does not match provider type '{typeof(T).Name}'");
            }
        }

        protected abstract void OnTake(T @object);

        protected abstract void OnReturn(T @object);
    }
}
