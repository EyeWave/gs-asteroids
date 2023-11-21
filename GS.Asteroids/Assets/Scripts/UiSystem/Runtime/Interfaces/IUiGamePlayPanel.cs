namespace GS.Asteroids.UiSystem
{
    internal interface IUiGamePlayPanel
    {
        void Show();

        void Hide();

        void RefreshPosition(string value);

        void RefreshRotation(string value);

        void RefreshSpeed(string value);

        void RefreshAlternativeFireCount(string value);

        void RefreshAlternativeFireCoolDown(float value);
    }
}
