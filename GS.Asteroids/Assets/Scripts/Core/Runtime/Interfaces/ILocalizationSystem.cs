using System;

namespace GS.Asteroids.Core.Interfaces
{
    public interface ILocalizationSystem : IDisposable
    {
        string Get(string key);
    }
}
