using GS.Asteroids.Core.Interfaces;
using System;

namespace GS.Asteroids.Core.Systems
{
    internal sealed class DrawSystemProvider : SystemCollectionProviderBase<IDrawable>, IRefreshable
    {
        private readonly IDrawSystem _drawSystem;

        internal DrawSystemProvider(IDrawSystem drawSystem, int capacity = 1024) : base(capacity)
        {
            _drawSystem = drawSystem ?? throw new ArgumentNullException(nameof(drawSystem));
        }

        public void Refresh()
        {
            _drawSystem.Draw(Collection);
        }
    }
}
