using AddressablesGenerated;
using Content.Features.AmmoSystem;
using Content.Features.BulletsPool;
using Content.Features.ConfigsSystem.Scripts;
using Content.Features.GameState.Scripts;
using Content.Features.VFXPools.Scripts;
using Core.EventBus;
using UnityEngine;
using Zenject;

namespace Content.Global.Scripts
{
    public class GameplayInstaller : MonoInstaller
    {
        [Header("Configs")]
        [SerializeField] private PlayerConfig _playerConfig;
        [SerializeField] private EnemyConfig _meleeEnemyConfig;
        [Header("Bullet Pool")]
        [SerializeField] private Transform bulletPoolParent;
        [SerializeField] private int bulletWarmUpCount = 20;
        [Header("Death Effects Pool")]
        [SerializeField] private Transform poolParent;
        [SerializeField] private int deathEffectsWarmUpCount = 10;
        
        private string effectAddress = Address.GameObjects.EnemyDeathEffect;
        private string bulletAddress = Address.GameObjects.Bullet;
        protected IEventBus _eventBus;
        
        [Inject]
        public void Construct(IEventBus eventBus)
        {
            _eventBus = eventBus;
        }
        
        public override void InstallBindings()
        {
            Container.Bind<PlayerConfig>().To<PlayerConfig>().FromInstance(_playerConfig).AsSingle();
            
            Container.Bind<EnemyConfig>().To<EnemyConfig>().FromInstance(_meleeEnemyConfig).AsSingle();
            
            Container.Bind<IAmmoSystem>().To<AmmoSystem>().AsSingle();
            
            Container.Bind<IBulletPool>().To<BulletPool>().AsSingle()
                .WithArguments(bulletAddress, bulletPoolParent)
                .NonLazy(); 
            
            Container.Bind<IDeathEffectPool>().To<DeathEffectPool>().AsSingle()
                .WithArguments(effectAddress, poolParent)
                .NonLazy(); 
        }
        
        public override async void Start()
        {
            var deathPool = Container.Resolve<IDeathEffectPool>();
            await deathPool.WarmUpAsync(deathEffectsWarmUpCount);
            var bulletsPool = Container.Resolve<IBulletPool>();
            await bulletsPool.WarmUpAsync(bulletWarmUpCount);
            _eventBus.Publish(new RespawnLevelPrefab());
        }
    }
}