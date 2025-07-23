using UnityEngine;
using Cysharp.Threading.Tasks;
using Zenject;
using System;
using Content.Features.GameState.Scripts;
using Content.Features.LevelProgressService.Scripts;
using Core.EventBus;
using Core.PrefabFactory;

namespace Content.Features.LevelSpawner.Scripts
{
    public class LevelSpawner : MonoBehaviour
    {
        [SerializeField] private bool spawnOnStart;
        [SerializeField] private Transform _spawnParent;
        
        private IEventBus _eventBus;
        private ILevelProgressService _levelProgressService;
        private IPrefabFactory _prefabsFactory;
        private GameObject _spawnedLevelInstance;
        
        [Inject]
        public void Construct(ILevelProgressService levelProgressService, IPrefabFactory factory, IEventBus eveBus)
        {
            _levelProgressService = levelProgressService;
            _prefabsFactory  = factory;
            _eventBus = eveBus;
            _eventBus.Subscribe<RespawnLevelPrefab>(RespawnNewLevel);
        }

        private void Start()
        {
            CallSpawnLogic();
        }

        private void OnDestroy()
        {
            _eventBus.Unsubscribe<RespawnLevelPrefab>(RespawnNewLevel);
        }

        private void RespawnNewLevel(RespawnLevelPrefab obj)
        {
            CallSpawnLogic();
        }
        
        private async void CallSpawnLogic()
        {
            if(spawnOnStart && _levelProgressService != null && _prefabsFactory != null)
            {
                await SpawnCurrentLevelAsync();
            }
        }

        public async UniTask SpawnCurrentLevelAsync()
        {
            DestroyCurrentLevel();
            int currentIndex = _levelProgressService.CurrentLevelIndex + 1;
            string address = $"Level_{currentIndex}";

            try
            {
                _spawnedLevelInstance = await _prefabsFactory.CreateAsync(address, _spawnParent);
            }
            catch (Exception ex)
            {
                Debug.LogError($"Failed to load level '{address}': {ex.Message}");
            }
        }

        public void DestroyCurrentLevel()
        {
            if (_spawnedLevelInstance != null)
            {
                Destroy(_spawnedLevelInstance);
                _spawnedLevelInstance = null;
            }
        }
    }
}