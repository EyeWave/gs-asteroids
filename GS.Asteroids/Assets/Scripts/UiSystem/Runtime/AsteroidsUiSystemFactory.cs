using GS.Asteroids.Core.Interfaces;
using System.Threading.Tasks;
using UnityEngine;

namespace GS.Asteroids.UiSystem
{
    public static class AsteroidsUiSystemFactory
    {
        public static async Task<IUiSystem> Create(IUiContextLoader uiContextLoader)
        {
            SimpleUiContext uiContext = await uiContextLoader.LoadAsync<SimpleUiContext>(nameof(SimpleUiContext));
            uiContext = Object.Instantiate(uiContext);
            uiContext.name = $"[{nameof(SimpleUiContext)}]";
            return new SimpleUiSystem
            (
                infoPanel: uiContext.InfoPanel,
                gamePlayPanel: uiContext.GamePlayPanel
            );
        }
    }
}
