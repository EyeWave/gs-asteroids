using GS.Asteroids.Core.Interfaces;
using GS.Asteroids.Core.Interfaces.UIContext;
using System;
using System.Linq;

namespace GS.Asteroids.Core.Systems
{
    internal sealed class GamePlayUiRefreshSystem : SystemCollectionProviderBase<IUIPlayer>, IRefreshable
    {
        private readonly IUiSystem _uiSystem;
        private readonly IUIAlternativeFire _alternativeFire;
        private readonly IDebugLogger _logger;

        private IUIPlayer _player;

        public GamePlayUiRefreshSystem(
            IUiSystem uiSystem,
            IUIAlternativeFire alternativeFire,
            IDebugLogger logger)
        {
            _uiSystem = uiSystem ?? throw new ArgumentNullException(nameof(uiSystem));
            _alternativeFire = alternativeFire ?? throw new ArgumentNullException(nameof(alternativeFire));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        public override void Init()
        {
            base.Init();

            _logger.Log("Start create UIGamePlayContext");

            _uiSystem.HideInfo();
            _uiSystem.ShowGamePlay();
            _player = Collection.Single();
        }

        public override void Dispose()
        {
            base.Dispose();

            _logger.Log("Stop create UIGamePlayContext");

            _player = null;
            _uiSystem.HideGamePlay();
        }

        public void Refresh()
        {
            if (_player == null)
                return;

            UiGamePlayContext context = new UiGamePlayContext
            (
                playerPosition: _player.Position,
                playerRotation: _player.Rotation,
                playerVelocity: _player.Velocity,
                alternativeFireCount: _alternativeFire.AlternativeFireCount,
                alternativeFireCoolDown: _alternativeFire.AlternativeFireCoolDown
            );

            _uiSystem.RefreshGamePlay(context);
        }
    }
}
