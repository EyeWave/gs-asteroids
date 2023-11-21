using UnityEngine;

namespace GS.Asteroids.UiSystem
{
    internal class SimpleUiContext : MonoBehaviour
    {
        internal IUiInfoPanel InfoPanel => _infoPanel;
        internal IUiGamePlayPanel GamePlayPanel => _gamePlayPanel;

        [SerializeField] private SimpleUiInfoPanel _infoPanel;
        [SerializeField] private SimpleUiGamePlayPanel _gamePlayPanel;
    }
}
