using UnityEngine;
using TMPro;

namespace GS.Asteroids.UiSystem
{
    internal class InfoPanel : MonoBehaviour
    {
        [SerializeField] private Canvas _canvas;
        [SerializeField] private TMP_Text _txtTitle;
        [SerializeField] private TMP_Text _txtDescription;

        internal void Show(string title, string description)
        {
            _txtTitle.SetText(title);
            _txtDescription.SetText(description);

            _canvas.enabled = true;
        }

        internal void Hide()
        {
            _canvas.enabled = false;
        }
    }
}
