using System;
using Content.Features.ConfigsSystem.Scripts;
using Content.Features.GameState.Scripts;
using Core.EventBus;
using Zenject;

namespace Content.Features.AmmoSystem
{
    public class AmmoSystem : IAmmoSystem, IDisposable
    {
        public bool CanShoot => CurrentAmmo > 0;
        public int CurrentAmmo => current;
        public int MaxAmmo => max;

        private int current;
        private int max;
        
        private IEventBus _eventBus;
        private PlayerConfig config;

        [Inject]
        public void Construct(IEventBus eventBus, PlayerConfig playerC)
        {
            _eventBus = eventBus;
            config = playerC;
            _eventBus.Subscribe<StartNextLevelGameEvent>(OnNextlevelStart);
            _eventBus.Subscribe<ReloadLevelGameEvent>(OnReloadLevel);
        }

        private void OnReloadLevel(ReloadLevelGameEvent obj)
        {
            if (config != null)
            {
                SetMaxAmmo(config.MaxAmmo);
            }
        }

        private void OnNextlevelStart(StartNextLevelGameEvent obj)
        {
            if (config != null)
            {
                SetMaxAmmo(config.MaxAmmo);
            }
        }

        public void SetMaxAmmo(int maxAmmo)
        {
            max = maxAmmo;
            current = max;
            SendEvents();
        }

        public bool TryConsumeAmmo()
        {
            if (current > 0)
            {
                current--;
                SendEvents();
                return true;
            }
            return false;
        }

        private void SendEvents()
        {
            AmmoChangedEvent ammoEvent = new AmmoChangedEvent();
            ammoEvent.maxAmmo = max;
            ammoEvent.currentAmmo = current;
            _eventBus.Publish(ammoEvent);
        }

        public void Dispose()
        {
            _eventBus.Unsubscribe<StartNextLevelGameEvent>(OnNextlevelStart);
            _eventBus.Unsubscribe<ReloadLevelGameEvent>(OnReloadLevel);
        }
    }
}