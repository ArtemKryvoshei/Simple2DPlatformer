using System.Threading;
using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.AddressableAssets;
using Zenject;

namespace Core.PrefabFactory
{
    public class PrefabFactory : IPrefabFactory
    {
        private readonly DiContainer _container;

        public PrefabFactory(DiContainer container)
        {
            _container = container;
        }

        public async UniTask<GameObject> CreateAsync(string address, Transform parent = null, CancellationToken token = default)
        {
            var handle = Addressables.LoadAssetAsync<GameObject>(address);
            await handle.ToUniTask(cancellationToken: token);

            if (handle.Status != UnityEngine.ResourceManagement.AsyncOperations.AsyncOperationStatus.Succeeded)
                throw new System.Exception($"[PrefabFactory] Failed to load prefab at address: {address}");

            var prefab = handle.Result;

            // Zenject создаёт инстанс и внедряет зависимости
            var instance = _container.InstantiatePrefab(prefab, parent);
            return instance;
        }
    }
}