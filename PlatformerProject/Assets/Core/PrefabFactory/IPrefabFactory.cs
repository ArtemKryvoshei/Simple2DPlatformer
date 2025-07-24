using System.Threading;
using Cysharp.Threading.Tasks;
using UnityEngine;
using Zenject;

namespace Core.PrefabFactory
{
    public interface IPrefabFactory
    {
        UniTask<GameObject> CreateAsync(string address, Transform parent = null, CancellationToken token = default);

        UniTask<GameObject> CreateAsync(string address, DiContainer containerOverride,
            Transform parent = null, CancellationToken token = default);
    }
}