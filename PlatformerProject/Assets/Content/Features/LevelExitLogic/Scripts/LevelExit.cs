using System;
using Content.Features.AmmoSystem;
using Content.Features.GameState.Scripts;
using Core.EventBus;
using UnityEngine;
using Zenject;

namespace Content.Features.LevelExitLogic.Scripts
{
    [RequireComponent(typeof(Collider2D))]
    public class LevelExit : MonoBehaviour
    {
        [Header("Detection Settings")]
        [SerializeField] private float detectionRadius = 5f;
        [SerializeField] private LayerMask targetLayer;
        
        private IEventBus _eventBus;
        
        [Inject]
        public void Construct(IEventBus eventBus)
        {
            _eventBus = eventBus;
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            Collider2D[] targets = Physics2D.OverlapCircleAll(transform.position, detectionRadius, targetLayer);
            if (targets.Length > 0)
                _eventBus.Publish(new OnLevelCompleteEvent());
        }
    }
}