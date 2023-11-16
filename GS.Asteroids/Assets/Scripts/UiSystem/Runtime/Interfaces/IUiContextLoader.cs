using System.Threading.Tasks;

namespace GS.Asteroids.UiSystem
{
    public interface IUiContextLoader
    {
        Task<T> LoadAsync<T>(string name) where T : class;
    }
}
