using System;
using Content.Features.GameState.Scripts;
using Content.Features.LevelProgressService.Scripts;
using Core.EventBus;
using TMPro;
using UnityEngine;
using Zenject;

namespace Content.Features.LevelTextDisplayer.Scripts
{
    public class LevelTextDisplayer : MonoBehaviour
    {
        [SerializeField] private TMP_Text _text;
        [SerializeField] private string _format = "Level {0}";

        private ILevelProgressService _levelProgressService;
        private IEventBus _eventBus;

        [Inject]
        public void Construct(ILevelProgressService levelProgressService, IEventBus eventBus)
        {
            _eventBus = eventBus;
            _levelProgressService = levelProgressService;
            
            _eventBus.Subscribe<ReloadLevelGameEvent>(UpdateLevelTextOnReload);
            _eventBus.Subscribe<StartNextLevelGameEvent>(UpdateLevelTextOnNewlevel);
        }

        private void UpdateLevelTextOnNewlevel(StartNextLevelGameEvent obj)
        {
            UpdateText();
        }

        private void UpdateLevelTextOnReload(ReloadLevelGameEvent obj)
        {
            UpdateText();
        }

        private void Start()
        {
            UpdateText();
        }

        private void UpdateText()
        {
            _text.text = string.Format(_format, _levelProgressService.CurrentLevelIndex + 1); 
        }

        private void OnDestroy()
        {
            _eventBus.Subscribe<ReloadLevelGameEvent>(UpdateLevelTextOnReload);
            _eventBus.Subscribe<StartNextLevelGameEvent>(UpdateLevelTextOnNewlevel);
        }
    }
}