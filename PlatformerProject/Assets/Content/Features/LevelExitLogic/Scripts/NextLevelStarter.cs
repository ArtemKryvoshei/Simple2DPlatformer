using UnityEngine;
using Cysharp.Threading.Tasks;
using Zenject;
using System;
using Content.Features.GameState.Scripts;
using Content.Features.LevelProgressService.Scripts;
using Core.EventBus;
using Core.PrefabFactory;


namespace Content.Features.LevelExitLogic.Scripts
{
    public class NextLevelStarter : MonoBehaviour
    { 
        private IEventBus _eventBus;
        private ILevelProgressService _levelProgressService;
        
        [Inject]
        public void Construct(ILevelProgressService levelProgressService, IEventBus eveBus)
        {
            _levelProgressService = levelProgressService;
            _eventBus = eveBus;
            _eventBus.Subscribe<StartNextLevelGameEvent>(RememberNextLevelAndRelaunch);
        }

        private void RememberNextLevelAndRelaunch(StartNextLevelGameEvent obj)
        {
            _levelProgressService.AdvanceToNextLevel();
            _eventBus.Publish(new ReloadLevelGameEvent());
            _eventBus.Publish(new RespawnLevelPrefab());
        }

        private void OnDestroy()
        {
            _eventBus.Unsubscribe<StartNextLevelGameEvent>(RememberNextLevelAndRelaunch);
        }
    }
}