using GS.Asteroids.Core.Entity;
using GS.Asteroids.Core.Interfaces;
using System;
using Vector3 = UnityEngine.Vector3;

namespace GS.Asteroids.Core.Systems
{
    internal sealed class TeleportSystem : SystemCollectionProviderBase<ITeleportable>, IRefreshable
    {
        private readonly ILevel _level;

        internal TeleportSystem(ILevel level, int capacity = 1024) : base(capacity)
        {
            _level = level ?? throw new ArgumentNullException(nameof(level));
        }

        public void Refresh()
        {
            foreach (ITeleportable @object in Collection)
                if (@object != null)
                    TryTeleport(@object);
        }

        private void TryTeleport(ITeleportable @object)
        {
            if (@object.Position.y > _level.Top + @object.Radius && CheckMoveDirection(@object.Velocity, Vector3.up))
                TeleportByY(@object, _level.Bottom - @object.Radius);
            else if (@object.Position.y < _level.Bottom - @object.Radius && CheckMoveDirection(@object.Velocity, Vector3.down))
                TeleportByY(@object, _level.Top + @object.Radius);
            else if (@object.Position.x < _level.Left - @object.Radius && CheckMoveDirection(@object.Velocity, Vector3.left))
                TeleportByX(@object, _level.Right + @object.Radius);
            else if (@object.Position.x > _level.Right + @object.Radius && CheckMoveDirection(@object.Velocity, Vector3.right))
                TeleportByX(@object, _level.Left - @object.Radius);
        }

        private bool CheckMoveDirection(Vector3 objectVelocity, Vector3 checkDirection)
        {
            return Vector3.Dot(checkDirection, objectVelocity) > 0.0f;
        }

        private void TeleportByX(ITeleportable @object, float value)
        {
            @object.Position = new Vector3(value, @object.Position.y);
        }

        private void TeleportByY(ITeleportable @object, float value)
        {
            @object.Position = new Vector3(@object.Position.x, value);
        }
    }
}
