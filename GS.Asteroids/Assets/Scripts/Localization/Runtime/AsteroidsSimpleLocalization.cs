using GS.Asteroids.Core;
using GS.Asteroids.Core.Interfaces;
using System.Collections.Generic;

namespace GS.Asteroids.LocalizationSystem
{
    internal sealed class AsteroidsSimpleLocalization : ILocalizationSystem
    {
        private readonly IDebugLogger _logger;
        private readonly IReadOnlyDictionary<string, string> _map;

        public AsteroidsSimpleLocalization(IDebugLogger logger)
        {
            _logger = logger ?? throw new System.ArgumentNullException(nameof(logger));
            _map = GetMap();
        }

        public string Get(string key)
        {
            if (!_map.TryGetValue(key, out string result))
            {
                _logger.LogWarning($"Not found localization for key '{key}'");
                result = $"%{key}%";
            }

            return result;
        }

        public void Dispose()
        {
            _logger.Log("Close localization");
        }

        private IReadOnlyDictionary<string, string> GetMap()
        {
            return new Dictionary<string, string>
            {
                {  AppLocalizationKeys.AppName, "Asteroids" },
                {  AppLocalizationKeys.PressKeyToStart, "Hold Space to start" },
                {  AppLocalizationKeys.GameOver, "Game over" },
                {  AppLocalizationKeys.PressKeyToContinue, "Hold space to continue" },
            };
        }
    }
}
