using GS.Asteroids.Core.Interfaces;
using System.Threading.Tasks;
using UnityEngine;

namespace GS.Asteroids.UiSystem
{
    public static class AsteroidsUiSystemFactory
    {
        public static async Task<IUiSystem> Create(IUiContextLoader uiContextLoader)
        {
            SimpleIUiContext uiContext = await uiContextLoader.LoadAsync<SimpleIUiContext>(nameof(SimpleIUiContext));
            uiContext = Object.Instantiate(uiContext);
            uiContext.name = $"[{nameof(SimpleIUiContext)}]";
            return new SimpleIUiSystem(uiContext);
        }
    }
}
