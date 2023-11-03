using GS.Asteroids.Core.Entity;
using GS.Asteroids.Core.Interfaces;
using System;
using Vector3 = UnityEngine.Vector3;

namespace GS.Asteroids.Core.Systems
{
    internal sealed class TeleportSystem : SystemCollectionProviderBase<ITeleportable>, IRefreshable
    {
        private readonly ILevel _level;

        internal TeleportSystem(ILevel level) : base()
        {
            _level = level ?? throw new ArgumentNullException(nameof(level));
        }

        public void Refresh()
        {
            foreach (ITeleportable entityOfCollection in Collection)
                if (entityOfCollection != null)
                    TryTeleport(entityOfCollection);
        }

        private void TryTeleport(ITeleportable entity)
        {
            if (entity.Position.y > _level.Top + entity.Radius && CheckTeleportDirection(entity.Velocity, Vector3.up))
                TeleportByY(entity, _level.Bottom - entity.Radius);
            else if (entity.Position.y < _level.Bottom - entity.Radius && CheckTeleportDirection(entity.Velocity, Vector3.down))
                TeleportByY(entity, _level.Top + entity.Radius);
            else if (entity.Position.x < _level.Left - entity.Radius && CheckTeleportDirection(entity.Velocity, Vector3.left))
                TeleportByX(entity, _level.Right + entity.Radius);
            else if (entity.Position.x > _level.Right + entity.Radius && CheckTeleportDirection(entity.Velocity, Vector3.right))
                TeleportByX(entity, _level.Left - entity.Radius);
        }

        private bool CheckTeleportDirection(Vector3 objectVelocity, Vector3 checkDirection)
        {
            return Vector3.Dot(checkDirection, objectVelocity) > 0.0f;
        }

        private void TeleportByX(ITeleportable entity, float value)
        {
            entity.Position = new Vector3(value, entity.Position.y);
        }

        private void TeleportByY(ITeleportable entity, float value)
        {
            entity.Position = new Vector3(entity.Position.x, value);
        }
    }
}
