using GS.Asteroids.Core.Interfaces;
using System;

namespace GS.Asteroids.UiSystem
{
    internal class SimpleIUiSystem : IUiSystem
    {
        private readonly InfoPanel _infoPanel;

        public SimpleIUiSystem(SimpleIUiContext uiContext)
        {
            _infoPanel = uiContext == null || uiContext.InfoPanel == null ? throw new ArgumentNullException(nameof(_infoPanel)) : uiContext.InfoPanel;
        }

        public void Dispose()
        {
        }

        public void ShowInfo(string title, string description)
        {
            _infoPanel.Show(title, description);
        }

        public void HideInfo()
        {
            _infoPanel.Hide();
        }
    }
}
