using System.Threading;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace Core.PrefabFactory
{
    public interface IPrefabFactory
    {
        UniTask<GameObject> CreateAsync(string address, Transform parent = null, CancellationToken token = default);
    }
}