using System;
using Content.Features.GameState.Scripts;
using Content.Features.LevelSpawner.Scripts;
using Content.Features.PlayerInput.Scripts;
using Core.EventBus;
using UnityEngine;
using Zenject;

namespace Content.Features.EnemyLogic.Scripts
{
    public class RecycleableEnemy : MonoBehaviour, IRecycleable
    {
        private Vector2 startPosition = Vector2.zero;
        
        private IEventBus _eventBus;
        
        [Inject]
        public void Construct(IEventBus eventBus)
        {
            _eventBus = eventBus;
            _eventBus.Subscribe<ReloadLevelGameEvent>(OnReloadLevel);
        }

        private void Start()
        {
            Activate();
        }

        private void OnReloadLevel(ReloadLevelGameEvent obj)
        {
            Recycle();
        }

        private void OnDestroy()
        {
            _eventBus.Unsubscribe<ReloadLevelGameEvent>(OnReloadLevel);
        }

        public void Activate()
        {
            if (startPosition == Vector2.zero)
            {
                startPosition = transform.position;
            }
            else
            {
                transform.position = startPosition;
            }
        }

        public void Deactive()
        {
            gameObject.SetActive(false);
        }

        public void Recycle()
        {
            Debug.Log("[RecycleableEnemy] Reload enemy");
            Activate();
            gameObject.SetActive(true);
        }
    }
}