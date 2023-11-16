using System;

namespace GS.Asteroids.Core.Interfaces
{
    public interface IUiSystem : IDisposable
    {
        void ShowInfo(string title, string description);
        void HideInfo();
    }
}
