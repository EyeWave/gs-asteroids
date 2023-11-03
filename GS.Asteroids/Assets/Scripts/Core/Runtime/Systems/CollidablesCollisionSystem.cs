using GS.Asteroids.Core.Entity;
using GS.Asteroids.Core.Interfaces;
using GS.Asteroids.Core.Interfaces.GamePlay;
using System;
using System.Collections.Generic;
using Vector3 = UnityEngine.Vector3;

namespace GS.Asteroids.Core.Systems
{
    internal sealed class CollidablesCollisionSystem : SystemCollectionProviderBase<ICollidable>, IRefreshable
    {
        private readonly ICollisionCreateProvider _collisionCreateProvider;
        private readonly HashSet<ICollidable> _markedEntities;

        internal CollidablesCollisionSystem(ICollisionCreateProvider collisionCreateProvider) : base()
        {
            _collisionCreateProvider = collisionCreateProvider ?? throw new ArgumentNullException(nameof(collisionCreateProvider));
            _markedEntities = new HashSet<ICollidable>(1024);
        }

        public void Refresh()
        {
            foreach (ICollidable firstEntity in Collection)
            {
                _markedEntities.Add(firstEntity);

                foreach (ICollidable secondEntity in Collection)
                {
                    if (_markedEntities.Contains(secondEntity))
                        continue;

                    if (firstEntity.GetType() != secondEntity.GetType())
                        if (Vector3.Distance(firstEntity.Position, secondEntity.Position) < firstEntity.Radius + secondEntity.Radius)
                            CreateCollision(firstEntity, secondEntity);
                }
            }

            _markedEntities.Clear();
        }

        private void CreateCollision(ICollidable firstEntity, ICollidable secondEntity)
        {
            _collisionCreateProvider.Create(firstEntity, secondEntity);
        }
    }
}
