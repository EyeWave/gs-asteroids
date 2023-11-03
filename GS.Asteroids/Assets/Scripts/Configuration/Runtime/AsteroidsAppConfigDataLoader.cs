using System.Threading.Tasks;
using UnityEngine;

namespace GS.Asteroids.Configuration
{
    internal sealed class AsteroidsAppConfigDataLoader : IAppConfigDataLoader<AsteroidsAppConfigData>
    {
        public Task<AsteroidsAppConfigData> LoadAsync()
        {
            ResourceRequest resourceRequest = Resources.LoadAsync<AsteroidsAppConfigData>(nameof(AsteroidsAppConfigData));
            TaskCompletionSource<AsteroidsAppConfigData> taskCompletionSource = new TaskCompletionSource<AsteroidsAppConfigData>();

            resourceRequest.completed += operation =>
            {
                if (resourceRequest.asset is AsteroidsAppConfigData result)
                    taskCompletionSource.SetResult(result);
                else
                    taskCompletionSource.SetResult(default);
            };

            return taskCompletionSource.Task;
        }
    }
}
