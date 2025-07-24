using System;
using Content.Features.BulletsPool;
using Content.Features.VFXPools.Scripts;
using Core.EventBus;
using UnityEngine;
using UnityEngine.Events;
using Zenject;

namespace Content.Features.EnemyLogic.Scripts
{
    [RequireComponent(typeof(Collider2D))]
    public class EnemyKillChecker : MonoBehaviour
    {
        public UnityEvent OnKilled;
        
        private IDeathEffectPool _deathEffectPool;
        private IBulletPool _bulletPool;
        
        [Inject]
        public void Construct(IDeathEffectPool effectsPool, IBulletPool bulletsPool)
        {
            _deathEffectPool = effectsPool;
            _bulletPool = bulletsPool;
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (_deathEffectPool == null || _bulletPool == null)
                return;
            
            if (_bulletPool.Contains(other.gameObject, true))
            {
                GameObject effect = _deathEffectPool.Rent();
                effect.transform.position = transform.position;
                effect.SetActive(true);
                effect.GetComponent<EffectAutoReturnToPool>().Init(_deathEffectPool);
                _bulletPool.Return(other.gameObject);
                OnKilled?.Invoke();
            }
        }
    }
}