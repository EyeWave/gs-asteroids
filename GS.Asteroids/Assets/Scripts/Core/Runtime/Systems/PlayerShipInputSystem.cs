using GS.Asteroids.Core.Entity;
using GS.Asteroids.Core.Interfaces;
using GS.Asteroids.Core.Interfaces.Configuration;
using System;
using Mathf = UnityEngine.Mathf;
using Vector2 = UnityEngine.Vector2;
using Vector3 = UnityEngine.Vector3;

namespace GS.Asteroids.Core.Systems
{
    internal sealed class PlayerShipInputSystem : SystemCollectionProviderBase<IPlayerInputHandler>, IRefreshable
    {
        private readonly IInputSystem _inputSystem;
        private readonly float _maxAcceleration;
        private readonly float _inertionMultipler;

        internal PlayerShipInputSystem(IAppConfigDataProvider appConfigDataProvider, IInputSystem inputSystem) : base()
        {
            _inputSystem = inputSystem ?? throw new ArgumentNullException(nameof(inputSystem));

            IPlayerConfig playerConfig = appConfigDataProvider?.GetConfig<IPlayerConfig>() ?? throw new ArgumentNullException(nameof(appConfigDataProvider));
            _maxAcceleration = playerConfig.MaxAcceleration;
            _inertionMultipler = playerConfig.InertionMultipler;
        }

        public void Refresh()
        {
            foreach (IPlayerInputHandler entityOfCollection in Collection)
                if (entityOfCollection != null)
                    HandleInput(entityOfCollection);
        }

        private void HandleInput(IPlayerInputHandler entity)
        {
            Vector2 moveInput = _inputSystem.GetMove();

            entity.AngularAcceleration = moveInput.x > 0.0f ?
                Mathf.Lerp(entity.AngularAcceleration, -_maxAcceleration, _inertionMultipler) :
                (moveInput.x < 0.0f ?
                    Mathf.Lerp(entity.AngularAcceleration, _maxAcceleration, _inertionMultipler) :
                    Mathf.Lerp(entity.AngularAcceleration, 0.0f, _inertionMultipler));

            entity.Acceleration = moveInput.y > 0.0f ?
                Mathf.Lerp(entity.Acceleration, _maxAcceleration, _inertionMultipler) :
                Mathf.Lerp(entity.Acceleration, 0.0f, _inertionMultipler);

            float angleDirection = (entity.Rotation + 90.0f) * Mathf.Deg2Rad;
            Vector3 direction = new Vector3(Mathf.Cos(angleDirection), Mathf.Sin(angleDirection));

            entity.Direction = direction;
            entity.Velocity = entity.Acceleration * direction;
        }
    }
}
