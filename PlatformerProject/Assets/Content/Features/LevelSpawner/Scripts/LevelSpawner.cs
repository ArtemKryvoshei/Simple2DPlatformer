using UnityEngine;
using Cysharp.Threading.Tasks;
using Zenject;
using System;
using Content.Features.LevelProgressService.Scripts;
using Core.PrefabFactory;

namespace Content.Features.LevelSpawner.Scripts
{
    public class LevelSpawner : MonoBehaviour
    {
        [SerializeField] private bool spawnOnStart;
        [SerializeField] private Transform _spawnParent;
        
        private ILevelProgressService _levelProgressService;
        private IPrefabFactory _prefabsFactory;
        private GameObject _spawnedLevelInstance;
        
        [Inject]
        public void Construct(ILevelProgressService levelProgressService, IPrefabFactory factory)
        {
            _levelProgressService = levelProgressService;
            _prefabsFactory  = factory;
        }

        private async void Start()
        {
            if(spawnOnStart && _levelProgressService != null && _prefabsFactory != null)
            {
                await SpawnCurrentLevelAsync();
            }
        }

        public async UniTask SpawnCurrentLevelAsync()
        {
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