using System;
using Content.Features.GameState.Scripts;
using Core.EventBus;
using UnityEngine;
using UnityEngine.Events;
using Zenject;

namespace Content.Features.PlayerActions.Scripts
{
    [RequireComponent(typeof(Collider2D))]
    public class PlayerDeathChecker : MonoBehaviour
    {
        [Header("Detection Settings")]
        [SerializeField] private float detectionRadius = 5f;
        [SerializeField] private LayerMask killableLayer;
        public UnityEvent OnPlayerDeath;
        public UnityEvent OnRevivePlayer;
        
        private IEventBus _eventBus;

        [Inject]
        public void Construct(IEventBus eventBus)
        {
            _eventBus = eventBus;
            _eventBus.Subscribe<ReloadLevelGameEvent>(@event => OnRevivePlayer?.Invoke());
            _eventBus.Subscribe<StartNextLevelGameEvent>(@event => OnRevivePlayer?.Invoke());
        }
        
        private void OnTriggerEnter2D(Collider2D other)
        {
            Collider2D[] targets = Physics2D.OverlapCircleAll(transform.position, detectionRadius, killableLayer);
            if (targets.Length > 0 && _eventBus != null)
            {
                _eventBus.Publish(new OnGameOverEvent());
                OnPlayerDeath?.Invoke();
            }
        }
        
        private void OnDestroy()
        {
            _eventBus.Unsubscribe<ReloadLevelGameEvent>(@event => OnRevivePlayer?.Invoke());
            _eventBus.Unsubscribe<StartNextLevelGameEvent>(@event => OnRevivePlayer?.Invoke());
        }
    }
}