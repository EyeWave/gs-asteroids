using System;
using System.Collections.Generic;

namespace GS.Asteroids.Core.Systems
{
    internal abstract class SystemCollectionProviderBase<T> : IDisposable
    {
        private readonly HashSet<T> _collection;

        protected IReadOnlyCollection<T> Collection => _collection;

        protected SystemCollectionProviderBase(int capacity = 1024)
        {
            _collection = new HashSet<T>(Math.Max(0, capacity));
        }

        public virtual void Dispose()
        {
            _collection.Clear();
        }

        internal virtual void Add(T @object)
        {
            _collection.Add(@object);
        }

        internal virtual void Remove(T @object)
        {
            _collection.Remove(@object);
        }
    }
}
