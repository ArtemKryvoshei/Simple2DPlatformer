using System;
using Core.EventBus;
using UnityEngine;
using Zenject;

namespace Content.Features.LevelSpawner.Scripts
{
    public class PlayerPlacer : MonoBehaviour
    {
        [SerializeField] private Transform _playerTransform;
        
        private IEventBus _eventBus;
        
        [Inject]
        public void Construct(IEventBus eventBus)
        {
            _eventBus = eventBus;
            _eventBus.Subscribe<PlacePlayerLevelEvent>(OnCallPlayerPlace);
        }

        private void OnCallPlayerPlace(PlacePlayerLevelEvent obj)
        {
            _playerTransform.position = obj.position;
        }

        private void OnDestroy()
        {
            _eventBus.Unsubscribe<PlacePlayerLevelEvent>(OnCallPlayerPlace);
        }
    }
}