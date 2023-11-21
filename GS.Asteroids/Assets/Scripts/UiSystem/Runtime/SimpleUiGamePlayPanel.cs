using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace GS.Asteroids.UiSystem
{
    internal class SimpleUiGamePlayPanel : MonoBehaviour, IUiGamePlayPanel
    {
        [SerializeField] private Canvas _canvas;
        [SerializeField] private TMP_Text _txtPosition;
        [SerializeField] private TMP_Text _txtRotation;
        [SerializeField] private TMP_Text _txtSpeed;
        [SerializeField] private TMP_Text _txtAlternativeFire;
        [SerializeField] private Image _imgAlternativeFireCoolDown;

        void IUiGamePlayPanel.Show()
        {
            _canvas.enabled = true;
        }

        void IUiGamePlayPanel.Hide()
        {
            if (_canvas)
                _canvas.enabled = false;
        }

        void IUiGamePlayPanel.RefreshPosition(string value)
        {
            _txtPosition.SetText(value);
        }

        void IUiGamePlayPanel.RefreshRotation(string value)
        {
            _txtRotation.SetText(value);
        }

        void IUiGamePlayPanel.RefreshSpeed(string value)
        {
            _txtSpeed.SetText(value);
        }

        void IUiGamePlayPanel.RefreshAlternativeFireCount(string value)
        {
            _txtAlternativeFire.SetText(value);
        }

        public void RefreshAlternativeFireCoolDown(float value)
        {
            _imgAlternativeFireCoolDown.fillAmount = value;
        }
    }
}
