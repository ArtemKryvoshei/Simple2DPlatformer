using Core.EventBus;
using TMPro;
using UnityEngine;
using Zenject;

namespace Content.Features.AmmoSystem
{
    public class AmmoUI : MonoBehaviour
    {
        [SerializeField] private TMP_Text ammoText;
        private IEventBus _eventBus;
        
        [Inject]
        public void Construct(IEventBus eventBus)
        {
            _eventBus = eventBus;
            _eventBus.Subscribe<AmmoChangedEvent>(OnAmmoChanged);
        }

        private void OnAmmoChanged(AmmoChangedEvent obj)
        {
            ammoText.text = $"{obj.currentAmmo} / {obj.maxAmmo}";
        }
        
        private void OnDestroy()
        {
            _eventBus.Unsubscribe<AmmoChangedEvent>(OnAmmoChanged);
        }
    }
}