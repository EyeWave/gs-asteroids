using GS.Asteroids.Core.Entity;
using System;
using System.Collections.Concurrent;

namespace GS.Asteroids.Core.Providers
{
    internal class ObjectPool<T> where T : IEntity
    {
        private readonly ConcurrentBag<T> _objects;
        private readonly Func<T> _objectGenerator;

        public ObjectPool(Func<T> objectGenerator)
        {
            _objectGenerator = objectGenerator ?? throw new ArgumentNullException(nameof(objectGenerator));
            _objects = new ConcurrentBag<T>();
        }

        public T Take()
        {
            if (!_objects.TryTake(out T item))
                item = _objectGenerator.Invoke();

            return item;
        }

        public void Return(T item)
        {
            _objects.Add(item);
        }
    }
}
