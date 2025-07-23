using Content.Features.AmmoSystem;
using Core.PrefabFactory;
using UnityEngine;
using Zenject;

namespace Content.Features.EnemySpawners.Scripts
{
    public class EnemySpawner : MonoBehaviour
    {
        [SerializeField] private Transform spawnPoint;
        [SerializeField] private string spawnEnemyName;
        
        private IPrefabFactory _factory;
        private DiContainer sceneContainer;

        [Inject]
        public void Construct(IPrefabFactory factory)
        {
            _factory = factory;
            var sceneContext = Object.FindObjectOfType<SceneContext>();
            sceneContainer = sceneContext.Container;
        }

        public async void SpawnMyEnemy()
        {
            if (_factory != null && !string.IsNullOrEmpty(spawnEnemyName) && spawnPoint != null)
            {
                GameObject spawnedEnemy = await _factory.CreateAsync(spawnEnemyName, sceneContainer, null);
                spawnedEnemy.transform.position = spawnPoint.position;
            }
        }
    }
}