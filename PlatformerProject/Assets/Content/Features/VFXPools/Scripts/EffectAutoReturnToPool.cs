using UnityEngine;

namespace Content.Features.VFXPools.Scripts
{
    public class EffectAutoReturnToPool : MonoBehaviour
    {
        [SerializeField] private float timeToLive = 2;
        private IDeathEffectPool _deathEffectPool;

        public void Init(IDeathEffectPool _effectPool)
        {
            _deathEffectPool = _effectPool;
            Invoke("ReturnToPool", timeToLive);
        }

        private void ReturnToPool()
        {
            _deathEffectPool.Return(this.gameObject);
        }
    }
}