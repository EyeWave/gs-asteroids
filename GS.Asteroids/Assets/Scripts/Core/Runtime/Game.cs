using GS.Asteroids.Core.Entity;
using GS.Asteroids.Core.Interfaces;
using GS.Asteroids.Core.Player;
using GS.Asteroids.Core.Systems;
using System;
using System.Collections.Generic;
using Mathf = UnityEngine.Mathf;
using Vector2 = UnityEngine.Vector2;
using Vector3 = UnityEngine.Vector3;

namespace GS.Asteroids.Core
{
    public class Game : IRefreshable, IDisposable
    {
        private readonly IRoot _root;
        private readonly ILevel _level;
        private readonly IDrawSystem _drawSystem;
        private readonly IInputSystem _inputSystem;
        private readonly IDebugLogger _logger;

        private readonly List<IDrawable> _drawables;

        private PlayerShip _playerShip;

        public Game(
            IRoot root,
            ILevel level,
            IDrawSystem drawSystem,
            IInputSystem inputSystem,
            IDebugLogger logger)
        {
            _root = root ?? throw new ArgumentNullException(nameof(root));
            _level = level ?? throw new ArgumentNullException(nameof(level));
            _drawSystem = drawSystem ?? throw new ArgumentNullException(nameof(drawSystem));
            _inputSystem = inputSystem ?? throw new ArgumentNullException(nameof(inputSystem));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));

            _inputSystem.Fire += OnFire;
            _inputSystem.AlternativeFire += OnAlternativeFire;

            TeleportSystem teleportSystem = new TeleportSystem(_level);
            DrawSystemProvider drawSystemProvider = new DrawSystemProvider(_drawSystem);
            RefreshDrawablePointsSystem refreshDrawablePointsSystem = new RefreshDrawablePointsSystem();

            _root.Install(_inputSystem);
            _root.Install(this);
            _root.Install(teleportSystem);
            _root.Install(refreshDrawablePointsSystem);
            _root.Install(drawSystemProvider);
            _root.Install(_logger);

            float playerRadius = 3.0f;
            EntityDrawablePointsContainer drawablePointsContainer = new EntityDrawablePointsContainer(radius: playerRadius, GetCorePointsOfPlayerShip(playerRadius));
            _playerShip = new PlayerShip(_level.GetStartPoint(), drawablePointsContainer);

            refreshDrawablePointsSystem.Add(_playerShip);
            teleportSystem.Add(_playerShip);
            drawSystemProvider.Add(_playerShip);

            _logger.Log(_level.ToString());
        }

        public void Dispose()
        {
            _inputSystem.Fire -= OnFire;
            _inputSystem.AlternativeFire -= OnAlternativeFire;
        }

        public void Refresh()
        {
            const float maxAcceleration = 2.0f;
            const float maxAngularAcceleration = 2.0f;
            const float lerpStepAcceleration = 0.05f;
            const float lerpStepAngularAcceleration = 0.05f;

            Vector2 playerInput = _inputSystem.GetMove();

            _playerShip.AngularAcceleration = playerInput.x > 0.0f ?
                Mathf.Lerp(_playerShip.AngularAcceleration, -maxAngularAcceleration, lerpStepAngularAcceleration) :
                (playerInput.x < 0.0f ?
                    Mathf.Lerp(_playerShip.AngularAcceleration, maxAngularAcceleration, lerpStepAngularAcceleration) :
                    Mathf.Lerp(_playerShip.AngularAcceleration, 0.0f, lerpStepAngularAcceleration));
            _playerShip.Rotation = (_playerShip.Rotation + _playerShip.AngularAcceleration) % 360;

            float angle = (_playerShip.Rotation + 90.0f) * Mathf.Deg2Rad;
            _playerShip.Acceleration = playerInput.y > 0.0f ?
                Mathf.Lerp(_playerShip.Acceleration, maxAcceleration, lerpStepAcceleration) :
                Mathf.Lerp(_playerShip.Acceleration, 0.0f, lerpStepAcceleration);
            _playerShip.Velocity = _playerShip.Acceleration * new Vector3(Mathf.Cos(angle), Mathf.Sin(angle));
            _playerShip.Position += _playerShip.Velocity;
        }

        private void OnFire()
        {
            _logger.Log($"<color=green>OnFire</color>");
        }

        private void OnAlternativeFire()
        {
            _logger.Log($"<color=red>OnAlternativeFire</color>");
        }

        private Vector3[] GetCorePointsOfPlayerShip(float radius)
        {
            return new Vector3[]
            {
                new Vector3(-radius, -radius),
                new Vector3(0.0f, radius),
                new Vector3(radius, -radius),
            };
        }
    }
}
