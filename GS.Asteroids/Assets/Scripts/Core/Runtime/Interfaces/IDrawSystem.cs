using System;
using System.Collections.Generic;

namespace GS.Asteroids.Core.Interfaces
{
    public interface IDrawSystem : IDisposable
    {
        void Draw(IEnumerable<IDrawable> drawables);
    }
}
