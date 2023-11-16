using UnityEngine;

namespace GS.Asteroids.UiSystem
{
    internal class SimpleIUiContext : MonoBehaviour
    {
        internal InfoPanel InfoPanel => _infoPanel;

        [SerializeField] private InfoPanel _infoPanel;
    }
}
