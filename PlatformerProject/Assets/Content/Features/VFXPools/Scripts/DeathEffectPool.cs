using Content.Features.BulletsPool;
using Core.PoolSystem;
using Core.PrefabFactory;
using UnityEngine;

namespace Content.Features.VFXPools.Scripts
{
    public class DeathEffectPool : GameObjectPool, IDeathEffectPool
    {
        public DeathEffectPool(IPrefabFactory prefabFactory, string address, Transform parent = null)
            : base(prefabFactory, address, parent) { }
    }
}