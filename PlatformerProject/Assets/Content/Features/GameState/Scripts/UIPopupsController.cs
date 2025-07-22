using System;
using Core.EventBus;
using UnityEngine;
using UnityEngine.Events;
using Zenject;

namespace Content.Features.GameState.Scripts
{
    public class UIPopupsController : MonoBehaviour
    {
        public UnityEvent OnPauseOpen;
        public UnityEvent OnPauseClose;
        
        public UnityEvent OnGameOver;
        public UnityEvent OnlevelComplete;
        
        public UnityEvent OnReloadlevel;
        
        private IEventBus _eventBus;
        
        [Inject]
        public void Construct(IEventBus eventBus)
        {
            _eventBus = eventBus;
            _eventBus.Subscribe<OnGameOverEvent>(OnGameOverProceed);
            _eventBus.Subscribe<OnLevelCompleteEvent>(OnLevelCompleteProceed);
            _eventBus.Subscribe<PauseGameEvent>(ProceedPause);
            _eventBus.Subscribe<UnpauseGameEvent>(ProceedUnpause);
            _eventBus.Subscribe<ReloadLevelGameEvent>(ProceedReloadLevel);
        }

        private void OnDestroy()
        {
            _eventBus.Unsubscribe<OnGameOverEvent>(OnGameOverProceed);
            _eventBus.Unsubscribe<OnLevelCompleteEvent>(OnLevelCompleteProceed);
            _eventBus.Unsubscribe<PauseGameEvent>(ProceedPause);
            _eventBus.Unsubscribe<UnpauseGameEvent>(ProceedUnpause);
            _eventBus.Unsubscribe<ReloadLevelGameEvent>(ProceedReloadLevel);
        }

        private void ProceedUnpause(UnpauseGameEvent obj)
        {
            OnPauseClose?.Invoke();
        }

        private void ProceedPause(PauseGameEvent obj)
        {
            OnPauseOpen?.Invoke();
        }

        private void OnLevelCompleteProceed(OnLevelCompleteEvent obj)
        {
            OnlevelComplete?.Invoke();
        }

        private void OnGameOverProceed(OnGameOverEvent obj)
        {
            OnGameOver?.Invoke();
        }
        
        private void ProceedReloadLevel(ReloadLevelGameEvent obj)
        {
            OnReloadlevel?.Invoke();
        }
    }
}