using System.Threading;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace Core.PoolSystem
{
    public interface IGameObjectPool
    {
        UniTask WarmUpAsync(int count, CancellationToken token = default);
        GameObject Rent();
        void Return(GameObject obj);
        bool Contains(GameObject obj, bool includeRented);
    }
}