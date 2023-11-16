using GS.Asteroids.Core.Entity;
using System;
using System.Collections.Generic;

namespace GS.Asteroids.Core.Systems
{
    internal abstract class DoubleSystemCollectionProviderBase<TFirst, TSecond> : SystemCollectionProviderBase<TFirst> where TFirst : IEntity
    {
        private readonly HashSet<TSecond> _collection;

        protected IReadOnlyCollection<TSecond> SecondCollection => _collection;

        protected DoubleSystemCollectionProviderBase(int capacity = 1024) : base(capacity)
        {
            _collection = new HashSet<TSecond>(Math.Max(0, capacity));
        }

        public override void Dispose()
        {
            _collection.Clear();

            base.Dispose();
        }

        public override void Add(IEntity entity)
        {
            if (entity is TSecond entityOfCollection)
                _collection.Add(entityOfCollection);

            base.Add(entity);
        }

        public override bool Remove(IEntity entity)
        {
            bool result = false;

            if (entity is TSecond entityOfCollection)
                result = _collection.Remove(entityOfCollection);

            result |= base.Remove(entity);

            return result;
        }
    }
}
