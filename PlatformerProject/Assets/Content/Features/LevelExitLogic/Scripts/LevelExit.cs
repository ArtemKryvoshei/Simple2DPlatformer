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
        [SerializeField] private LayerMask targetLayer;

        private IEventBus _eventBus;

        [Inject]
        public void Construct(IEventBus eventBus)
        {
            _eventBus = eventBus;
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (((1 << other.gameObject.layer) & targetLayer) != 0)
            {
                _eventBus.Publish(new OnLevelCompleteEvent());
            }
        }
    }

}