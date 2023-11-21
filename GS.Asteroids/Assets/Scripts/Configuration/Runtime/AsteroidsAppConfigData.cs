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
        public IChipConfig ChipConfig => _chipConfig;
        public IUfoConfig UfoConfig => _ufoConfig;
        public IBulletConfig BulletConfig => _bulletConfig;
        public ILaserConfig LaserConfig => _laserConfig;

        [SerializeField] private PlayerConfig _playerConfig;
        [SerializeField] private AsteroidConfig _asteroidConfig;
        [SerializeField] private ChipConfig _chipConfig;
        [SerializeField] private UfoConfig _ufoConfig;
        [SerializeField] private BulletConfig _bulletConfig;
        [SerializeField] private LaserConfig _laserConfig;
    }
}
