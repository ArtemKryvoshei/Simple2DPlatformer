using Core.PoolSystem;
using Core.PrefabFactory;
using UnityEngine;

namespace Content.Features.BulletsPool
{
    public class BulletPool : GameObjectPool, IBulletPool
    {
        public BulletPool(IPrefabFactory prefabFactory, string address, Transform parent = null)
            : base(prefabFactory, address, parent) { }
    }
}