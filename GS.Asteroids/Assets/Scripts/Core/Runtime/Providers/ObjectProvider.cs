using GS.Asteroids.Core.Entity;
using GS.Asteroids.Core.Interfaces.GamePlay;
using System;
using System.Collections.Generic;
using Mathf = UnityEngine.Mathf;

namespace GS.Asteroids.Core.Providers
{
    internal class ObjectProvider : IObjectProvider
    {
        private readonly IReadOnlyDictionary<RuntimeTypeHandle, IObjectProvider<IEntity>> _objectProviders;
        private readonly IList<IEntity> _activeObjects;

        public ObjectProvider(params IObjectProvider<IEntity>[] objectProviders)
        {
            Dictionary<RuntimeTypeHandle, IObjectProvider<IEntity>> providers = new(Mathf.NextPowerOfTwo(objectProviders.Length));

            foreach (IObjectProvider<IEntity> objectProvider in objectProviders)
                providers.Add(objectProvider.EntityType.TypeHandle, objectProvider);

            _objectProviders = providers;
            _activeObjects = new List<IEntity>(1024);
        }

        public T Take<T>() where T : class, IEntity
        {
            IEntity result;
            Type type = typeof(T);

            if (_objectProviders.TryGetValue(type.TypeHandle, out IObjectProvider<IEntity> objectProvider))
                result = objectProvider.Take();
            else
                throw new NotImplementedException($"Unknown type {type.Name}");

            _activeObjects.Add(result);

            return result as T;
        }

        public void Return(IEntity entity)
        {
            Type type = entity.GetType();

            if (_objectProviders.TryGetValue(type.TypeHandle, out IObjectProvider<IEntity> objectProvider))
                objectProvider.Return(entity);
            else
                throw new NotImplementedException($"Unknown type {type.Name}");

            _activeObjects.Remove(entity);
        }

        public void Dispose()
        {
            while (_activeObjects.Count > 0)
                Return(_activeObjects[0]);
        }
    }
}
