using GS.Asteroids.Core.Entity;
using GS.Asteroids.Core.Interfaces;
using Mathf = UnityEngine.Mathf;
using Random = UnityEngine.Random;
using Vector3 = UnityEngine.Vector3;

namespace GS.Asteroids.Core.Systems
{
    internal sealed class AsteroidInputSystem : SystemCollectionProviderBase<IAsteroidInputHandler>, IRefreshable
    {
        private const float TwoPi = Mathf.PI * 2;

        internal AsteroidInputSystem() : base()
        {
        }

        public void Refresh()
        {
            foreach (IAsteroidInputHandler entityOfCollection in Collection)
                if (entityOfCollection != null)
                    HandleInput(entityOfCollection);
        }

        private void HandleInput(IAsteroidInputHandler entity)
        {
            if (entity.Velocity != Vector3.zero)
                return;

            float direction = Random.Range(0.0f, TwoPi);
            entity.Velocity = entity.Acceleration * new Vector3(Mathf.Cos(direction), Mathf.Sin(direction));
        }
    }
}
