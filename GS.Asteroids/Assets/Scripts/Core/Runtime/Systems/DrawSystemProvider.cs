using GS.Asteroids.Core.Entity;
using GS.Asteroids.Core.Interfaces;
using System;

namespace GS.Asteroids.Core.Systems
{
    internal sealed class DrawSystemProvider : SystemCollectionProviderBase<IDrawableEntity>, IRefreshable
    {
        private readonly IDrawSystem _drawSystem;

        internal DrawSystemProvider(IDrawSystem drawSystem) : base()
        {
            _drawSystem = drawSystem ?? throw new ArgumentNullException(nameof(drawSystem));
        }

        public void Refresh()
        {
            _drawSystem.Draw(Collection);
        }

        public override void Dispose()
        {
            base.Dispose();

            _drawSystem.Draw(Collection);
        }
    }
}
