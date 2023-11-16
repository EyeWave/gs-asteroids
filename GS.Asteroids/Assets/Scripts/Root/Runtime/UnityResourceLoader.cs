using GS.Asteroids.Configuration.Interfaces;
using GS.Asteroids.UiSystem;
using System.Threading.Tasks;
using UnityEngine;

namespace GS.Asteroids.Root
{
    internal sealed class UnityResourceLoader : IAppConfigDataLoader, IUiContextLoader
    {
        public Task<T> LoadAsync<T>(string name) where T : class
        {
            ResourceRequest resourceRequest = Resources.LoadAsync<Object>(name);
            TaskCompletionSource<T> taskCompletionSource = new TaskCompletionSource<T>();

            resourceRequest.completed += _ =>
            {
                T result = default;

                if (resourceRequest.asset is T resultAsset)
                    result = resultAsset;
                else if (resourceRequest.asset is GameObject resultGameObject)
                    result = resultGameObject.GetComponent<T>();

                taskCompletionSource.SetResult(result);
            };

            return taskCompletionSource.Task;
        }
    }
}
