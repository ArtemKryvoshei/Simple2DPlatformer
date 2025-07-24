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
        [SerializeField] private LayerMask killableLayer;
        public UnityEvent OnPlayerDeath;
        public UnityEvent OnRevivePlayer;

        private IEventBus _eventBus;

        [Inject]
        public void Construct(IEventBus eventBus)
        {
            _eventBus = eventBus;
            _eventBus.Subscribe<ReloadLevelGameEvent>(_ => OnRevivePlayer?.Invoke());
            _eventBus.Subscribe<StartNextLevelGameEvent>(_ => OnRevivePlayer?.Invoke());
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (((1 << other.gameObject.layer) & killableLayer) != 0)
            {
                _eventBus.Publish(new OnGameOverEvent());
                OnPlayerDeath?.Invoke();
            }
        }

        private void OnDestroy()
        {
            _eventBus.Unsubscribe<ReloadLevelGameEvent>(_ => OnRevivePlayer?.Invoke());
            _eventBus.Unsubscribe<StartNextLevelGameEvent>(_ => OnRevivePlayer?.Invoke());
        }
    }

}