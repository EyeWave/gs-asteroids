using GS.Asteroids.Core.Entity;
using GS.Asteroids.Core.Interfaces.GamePlay;
using System;
using System.Collections.Generic;

namespace GS.Asteroids.Core.Systems
{
    internal abstract class SystemCollectionProviderBase<T> : IEntityProvider, IDisposable where T : IEntity
    {
        private readonly HashSet<T> _collection;

        protected IReadOnlyCollection<T> Collection => _collection;

        protected SystemCollectionProviderBase(int capacity = 1024)
        {
            _collection = new HashSet<T>(Math.Max(0, capacity));
        }

        public virtual void Init()
        {
        }

        public virtual void Dispose()
        {
            _collection.Clear();
        }

        public virtual void Add(IEntity entity)
        {
            if (entity is T entityOfCollection)
                _collection.Add(entityOfCollection);
        }

        public void AddRange(IEnumerable<IEntity> entities)
        {
            foreach (IEntity entity in entities)
                Add(entity);
        }

        public virtual bool Remove(IEntity entity)
        {
            bool result = false;

            if (entity is T entityOfCollection)
                result = _collection.Remove(entityOfCollection);

            return result;
        }
    }
}
