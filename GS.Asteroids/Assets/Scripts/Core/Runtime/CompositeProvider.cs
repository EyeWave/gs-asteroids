using GS.Asteroids.Core.Entity;
using GS.Asteroids.Core.Interfaces;
using GS.Asteroids.Core.Interfaces.GamePlay;
using GS.Asteroids.Core.Objects;
using System;
using System.Collections.Generic;
using System.Linq;

namespace GS.Asteroids.Core
{
    internal sealed class CompositeProvider : IObjectProvider, IEntityProvider, ISystemProvider, ICollisionCreateProvider, ICollisionProcessProvider, IResultProvider
    {
        private readonly IRoot _root;
        private readonly IObjectProvider _objectProvider;
        private readonly HashSet<IEntity> _entities;
        private readonly HashSet<IEntityProvider> _entityProviders;
        private readonly HashSet<Collision> _collisions;

        private int _result = 0;

        IEnumerable<Collision> ICollisionProcessProvider.Collisions => _collisions;

        int IResultProvider.Result => _result;

        internal CompositeProvider(IRoot root, IObjectProvider objectProvider)
        {
            _root = root ?? throw new ArgumentNullException(nameof(root));
            _objectProvider = objectProvider ?? throw new ArgumentNullException(nameof(objectProvider));

            _entities = new HashSet<IEntity>(1024);
            _entityProviders = new HashSet<IEntityProvider>(128);
            _collisions = new HashSet<Collision>(1024);
        }

        void ISystem.Init()
        {
            _entities.Clear();
            _entityProviders.Clear();
            _collisions.Clear();
        }

        public void Dispose()
        {
            _objectProvider.Dispose();

            while (_entities.Count > 0)
                (this as IEntityProvider).Remove(_entities.Last());
        }

        T IObjectProvider.Take<T>()
        {
            return _objectProvider.Take<T>();
        }

        void IObjectProvider.Return(IEntity entity)
        {
            _objectProvider.Return(entity);
        }

        void IEntityProvider.Add(IEntity entity)
        {
            if (_entities.Add(entity))
                foreach (IEntityProvider entityProvider in _entityProviders)
                    entityProvider.Add(entity);
        }

        void IEntityProvider.AddRange(IEnumerable<IEntity> entities)
        {
            foreach (IEntity entity in entities)
                (this as IEntityProvider).Add(entity);
        }

        bool IEntityProvider.Remove(IEntity entity)
        {
            bool result = _entities.Remove(entity);

            if (result)
                foreach (IEntityProvider entityProvider in _entityProviders)
                    entityProvider.Remove(entity);

            return result;
        }

        void ISystemProvider.Add(ISystem system)
        {
            _root.Install(system);

            if (system is IEntityProvider entityProvider)
                AddEntityProvider(entityProvider);
        }

        void ISystemProvider.Remove(ISystem system)
        {
            _root.Uninstall(system);

            if (system is IEntityProvider entityProvider)
                RemoveEntityProvider(entityProvider);
        }

        void ICollisionCreateProvider.Create(ICollidable first)
        {
            _collisions.Add(new Collision(first));
        }

        void ICollisionCreateProvider.Create(ICollidable first, ICollidable second)
        {
            _collisions.Add(new Collision(first, second));
        }

        void ICollisionProcessProvider.Clear()
        {
            _collisions.Clear();
        }

        void IResultProvider.AddReward(int value)
        {
            _result += value;
        }

        void IResultProvider.Clear()
        {
            _result = 0;
        }

        private void AddEntityProvider(IEntityProvider entityProvider)
        {
            _entityProviders.Add(entityProvider);

            foreach (IEntity entity in _entities)
                entityProvider.Add(entity);
        }

        private void RemoveEntityProvider(IEntityProvider entityProvider)
        {
            _entityProviders.Remove(entityProvider);

            foreach (IEntity entity in _entities)
                entityProvider.Remove(entity);
        }
    }
}
