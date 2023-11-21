using GS.Asteroids.Core.Entity;
using GS.Asteroids.Core.Interfaces;
using GS.Asteroids.Core.Interfaces.Configuration;
using GS.Asteroids.Core.Interfaces.GamePlay;
using GS.Asteroids.Core.Objects;
using System;
using System.Collections.Generic;

namespace GS.Asteroids.Core.Systems
{
    internal sealed class ResultCalculationSystem : ISystem, IRefreshable
    {
        private readonly int _asteroidReward;
        private readonly int _chipReward;
        private readonly int _ufoReward;
        private readonly ICollisionProcessProvider _collisionProcessProvider;
        private readonly IResultProvider _resultProvider;

        internal ResultCalculationSystem(
            IAppConfigDataProvider appConfigDataProvider,
            ICollisionProcessProvider collisionProcessProvider,
            IResultProvider resultProvider)
        {
            if (appConfigDataProvider is null)
                throw new ArgumentNullException(nameof(appConfigDataProvider));

            IAsteroidConfig asteroidConfig = appConfigDataProvider.GetConfig<IAsteroidConfig>() ?? throw new ArgumentNullException(nameof(IAsteroidConfig));
            IChipConfig chipConfig = appConfigDataProvider.GetConfig<IChipConfig>() ?? throw new ArgumentNullException(nameof(IChipConfig));
            IUfoConfig ufoConfig = appConfigDataProvider.GetConfig<IUfoConfig>() ?? throw new ArgumentNullException(nameof(IUfoConfig));

            _asteroidReward = asteroidConfig.Reward;
            _chipReward = chipConfig.Reward;
            _ufoReward = ufoConfig.Reward;

            _collisionProcessProvider = collisionProcessProvider ?? throw new ArgumentNullException(nameof(collisionProcessProvider));
            _resultProvider = resultProvider ?? throw new ArgumentNullException(nameof(resultProvider));
        }

        public void Refresh()
        {
            foreach (Collision collision in _collisionProcessProvider.Collisions)
                _resultProvider.AddReward(GetCollisionReward(collision));
        }

        private int GetCollisionReward(Collision collision)
        {
            int result = GetCollisionReward(collision.First, collision.Second);

            if (result == 0)
                result = GetCollisionReward(collision.Second, collision.First);

            return result;
        }

        private int GetCollisionReward(ICollidable firstCollidable, ICollidable secondCollidable)
        {
            int result = 0;

            if (firstCollidable is Bullet or Laser)
                result = secondCollidable switch
                {
                    Asteroid => _asteroidReward,
                    Chip => _chipReward,
                    Ufo => _ufoReward,
                    _ => 0
                };

            return result;
        }
    }
}
