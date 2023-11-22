using GS.Asteroids.Core.Interfaces;
using GS.Asteroids.Core.Interfaces.UIContext;
using System;
using Mathf = UnityEngine.Mathf;
using Vector2 = UnityEngine.Vector2;

namespace GS.Asteroids.UiSystem
{
    internal sealed class SimpleUiSystem : IUiSystem
    {
        private readonly IUiInfoPanel _infoPanel;
        private readonly IUiGamePlayPanel _gamePlayPanel;

        private Vector2 _previousPlayerPosition = Vector2.zero;
        private float _previousPlayerRotation = 0.0f;
        private Vector2 _previousPlayerVelocity = Vector2.zero;
        private int _previousAlternativeFireCount = 0;
        private float _previousAlternativeFireCoolDown = 0.0f;

        public SimpleUiSystem(IUiInfoPanel infoPanel, IUiGamePlayPanel gamePlayPanel)
        {
            _infoPanel = infoPanel == null ? throw new ArgumentNullException(nameof(infoPanel)) : infoPanel;
            _gamePlayPanel = gamePlayPanel == null ? throw new ArgumentNullException(nameof(gamePlayPanel)) : gamePlayPanel;
        }

        public void Dispose()
        {
        }

        public void ShowInfo(UiInfoContext context)
        {
            _infoPanel.Show(context.Title, context.Description);
        }

        public void HideInfo()
        {
            _infoPanel.Hide();
        }

        public void ShowGamePlay()
        {
            _gamePlayPanel.Show();
        }

        public void RefreshGamePlay(UiGamePlayContext context)
        {
            if (_previousPlayerPosition != context.PlayerPosition)
            {
                _gamePlayPanel.RefreshPosition($"[{context.PlayerPosition.x:0.0}:{context.PlayerPosition.y:0.0}]");
                _previousPlayerPosition = context.PlayerPosition;
            }

            if (!Mathf.Approximately(_previousPlayerRotation, context.PlayerRotation))
            {
                _gamePlayPanel.RefreshRotation($"{context.PlayerRotation:0.0}");
                _previousPlayerRotation = context.PlayerRotation;
            }

            if (_previousPlayerVelocity != context.PlayerVelocity)
            {
                _gamePlayPanel.RefreshSpeed($"{context.PlayerVelocity.magnitude:0.0}");
                _previousPlayerVelocity = context.PlayerVelocity;
            }

            if (_previousAlternativeFireCount != context.AlternativeFireCount)
            {
                _gamePlayPanel.RefreshAlternativeFireCount($"{context.AlternativeFireCount}");
                _previousAlternativeFireCount = context.AlternativeFireCount;
            }

            float valueAlternativeFireCoolDown = Mathf.Clamp01(context.AlternativeFireCoolDown);
            if (!Mathf.Approximately(_previousAlternativeFireCoolDown, valueAlternativeFireCoolDown))
            {
                _gamePlayPanel.RefreshAlternativeFireCoolDown(valueAlternativeFireCoolDown);
                _previousAlternativeFireCoolDown = valueAlternativeFireCoolDown;
            }
        }

        public void HideGamePlay()
        {
            _gamePlayPanel.Hide();
        }
    }
}
