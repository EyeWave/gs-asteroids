using GS.Asteroids.Core.Interfaces;
using GS.Asteroids.Core.Interfaces.Configuration;
using GS.Asteroids.Core.Interfaces.GamePlay;
using GS.Asteroids.Core.Interfaces.UIContext;
using GS.Asteroids.Core.Utils;
using System;
using Mathf = UnityEngine.Mathf;
using Vector2 = UnityEngine.Vector2;

namespace GS.Asteroids.Core.Systems
{
    internal sealed class InputSystemDecorator : IInputSystem, ISystem, IRefreshable, IUIAlternativeFire
    {
        public event Action Fire;
        public event Action AlternativeFire;
        public event Action Exit;

        public int AlternativeFireCount => _alternativeFireCount;
        public float AlternativeFireCoolDown { get; private set; }

        private readonly IInputSystem _inputSystem;
        private readonly IDebugLogger _logger;
        private readonly int _alternativeFireCountMax;
        private readonly float _alternativeFireCoolDownIntervalMillisec;

        private int _alternativeFireCount;
        private float _alternativeFireCoolDown;
        private DateTime _lastRefresh;

        private DateTime Now => DateTime.Now;

        internal InputSystemDecorator(
            IAppConfigDataProvider appConfigDataProvider,
            IInputSystem inputSystem,
            IDebugLogger logger) : base()
        {
            _inputSystem = inputSystem ?? throw new ArgumentNullException(nameof(inputSystem));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));

            ILaserConfig config = appConfigDataProvider?.GetConfig<ILaserConfig>() ?? throw new ArgumentNullException(nameof(ILaserConfig));
            _alternativeFireCountMax = config.CountMax;
            _alternativeFireCoolDownIntervalMillisec = config.CoolDownIntervalSec * 1000.0f;
        }

        public void Init()
        {
            _inputSystem.Fire += OnInputFire;
            _inputSystem.AlternativeFire += OnInputAlternativeFire;
            _inputSystem.Exit += OnInputExit;

            _alternativeFireCount = _alternativeFireCountMax;
            _alternativeFireCoolDown = 0.0f;
            _lastRefresh = Now;

            AlternativeFireCoolDown = MathUtils.ReMap(_alternativeFireCoolDown, _alternativeFireCoolDownIntervalMillisec, 0.0f, 0.0f, 1.0f);
        }

        public void Dispose()
        {
            _inputSystem.Fire -= OnInputFire;
            _inputSystem.AlternativeFire -= OnInputAlternativeFire;
            _inputSystem.Exit -= OnInputExit;
        }

        public void Refresh()
        {
            if (_alternativeFireCount < _alternativeFireCountMax)
            {
                float deltaMilliseconds = (float)(Now - _lastRefresh).TotalMilliseconds;

                _alternativeFireCoolDown = Mathf.Approximately(_alternativeFireCoolDown, 0.0f) ?
                    (float)(Now.AddMilliseconds(_alternativeFireCoolDownIntervalMillisec) - Now).TotalMilliseconds :
                    _alternativeFireCoolDown - deltaMilliseconds;

                if (_alternativeFireCoolDown <= 0.0f)
                {
                    _alternativeFireCoolDown = 0.0f;
                    _alternativeFireCount++;
                }

                AlternativeFireCoolDown = MathUtils.ReMap(_alternativeFireCoolDown, _alternativeFireCoolDownIntervalMillisec, 0.0f, 0.0f, 1.0f);
            }

            _lastRefresh = Now;
        }

        public Vector2 GetMove()
        {
            return _inputSystem.GetMove();
        }

        private void OnInputFire()
        {
            Fire?.Invoke();
        }

        private void OnInputAlternativeFire()
        {
            if (_alternativeFireCount > 0)
            {
                _alternativeFireCount--;
                AlternativeFire?.Invoke();

                _logger.Log("AlternativeFire");
            }
        }

        private void OnInputExit()
        {
            Exit?.Invoke();
        }
    }
}
