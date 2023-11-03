using System.Threading.Tasks;

namespace GS.Asteroids.Configuration
{
    internal interface IAppConfigDataLoader<T>
    {
        Task<T> LoadAsync();
    }
}
