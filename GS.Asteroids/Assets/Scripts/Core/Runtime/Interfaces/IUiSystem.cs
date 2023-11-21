using GS.Asteroids.Core.Interfaces.UIContext;
using System;

namespace GS.Asteroids.Core.Interfaces
{
    public interface IUiSystem : IDisposable
    {
        void ShowInfo(UiInfoContext context);

        void HideInfo();

        void ShowGamePlay();

        void RefreshGamePlay(UiGamePlayContext context);

        void HideGamePlay();
    }
}
