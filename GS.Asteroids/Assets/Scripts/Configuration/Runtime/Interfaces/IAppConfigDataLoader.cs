using System.Threading.Tasks;

namespace GS.Asteroids.Configuration.Interfaces
{
    public interface IAppConfigDataLoader
    {
        Task<T> LoadAsync<T>(string name) where T : class;
    }
}
