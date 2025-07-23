using System;
using UnityEngine;
using System.Collections.Generic;
using Content.Features.EnemySpawners.Scripts;
using Content.Features.GameState.Scripts;
using Content.Features.PlayerInput.Scripts;
using Core.EventBus;
using Zenject;

namespace Content.Features.LevelSpawner.Scripts
{
    public class LevelController : MonoBehaviour
    {
        [SerializeField] private Transform _playerSpawnPoint;
        [SerializeField] private EnemySpawner[] enemiesSpawners;

        private IEventBus _eventBus;
        
        [Inject]
        public void Construct(IEventBus eventBus)
        {
            _eventBus = eventBus;
            _eventBus.Subscribe<ReloadLevelGameEvent>(OnReloadLevel);
            _eventBus.Subscribe<OnLevelCompleteEvent>(OnLevelCompltedUnsubscribe);
        }

        private void OnLevelCompltedUnsubscribe(OnLevelCompleteEvent obj)
        {
            _eventBus.Unsubscribe<ReloadLevelGameEvent>(OnReloadLevel);
        }

        private void Start()
        {
            CallLevelLoadedEvents();
            foreach (var spawner in enemiesSpawners)
            {
                spawner.SpawnMyEnemy();
            }
        }
        
        private void OnDestroy()
        {
            _eventBus.Unsubscribe<ReloadLevelGameEvent>(OnReloadLevel);
        }
        
        private void OnReloadLevel(ReloadLevelGameEvent obj)
        {
            CallLevelLoadedEvents();
        }
        
        private void CallLevelLoadedEvents()
        {
            if (_eventBus != null)
            {
                PlacePlayerLevelEvent placePlayerLevelEvent = new PlacePlayerLevelEvent();
                placePlayerLevelEvent.position = _playerSpawnPoint.position;
                _eventBus.Publish(placePlayerLevelEvent);
            }
        }
    }
}