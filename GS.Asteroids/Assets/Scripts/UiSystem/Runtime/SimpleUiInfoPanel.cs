using TMPro;
using UnityEngine;

namespace GS.Asteroids.UiSystem
{
    internal sealed class SimpleUiInfoPanel : MonoBehaviour, IUiInfoPanel
    {
        [SerializeField] private Canvas _canvas;
        [SerializeField] private TMP_Text _txtTitle;
        [SerializeField] private TMP_Text _txtDescription;

        void IUiInfoPanel.Show(string title, string description)
        {
            _txtTitle.SetText(title);
            _txtDescription.SetText(description);

            _canvas.enabled = true;
        }

        void IUiInfoPanel.Hide()
        {
            if (_canvas)
                _canvas.enabled = false;
        }
    }
}
