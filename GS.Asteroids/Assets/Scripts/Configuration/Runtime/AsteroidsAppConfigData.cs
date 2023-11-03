using GS.Asteroids.Configuration.DataImplementation;
using GS.Asteroids.Core.Interfaces.Configuration;
using UnityEngine;

namespace GS.Asteroids.Configuration
{
    [CreateAssetMenu(fileName = nameof(AsteroidsAppConfigData), menuName = nameof(AsteroidsAppConfigData))]
    internal sealed class AsteroidsAppConfigData : ScriptableObject
    {
        public IPlayerConfig PlayerConfig => _playerConfig;
        public IAsteroidConfig AsteroidConfig => _asteroidConfig;
        public IBulletConfig BulletConfig => _bulletConfig;

        [SerializeField] private PlayerConfig _playerConfig;
        [SerializeField] private AsteroidConfig _asteroidConfig;
        [SerializeField] private BulletConfig _bulletConfig;
    }
}
