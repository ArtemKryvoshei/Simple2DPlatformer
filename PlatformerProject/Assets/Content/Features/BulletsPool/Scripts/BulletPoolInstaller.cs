using AddressablesGenerated;
using UnityEngine;
using Zenject;

namespace Content.Features.BulletsPool
{
    public class BulletPoolInstaller : MonoInstaller
    {
        [SerializeField] private Transform bulletPoolParent;
        [SerializeField] private int bulletWarmUpCount = 20;
        
        private string bulletAddress = Address.GameObjects.Bullet;

        public override void InstallBindings()
        {
            Container.Bind<IBulletPool>().To<BulletPool>().AsSingle()
                .WithArguments(bulletAddress, bulletPoolParent)
                .NonLazy(); // чтобы создать сразу, либо warm-up вызвать вручную позже
        }

        public override async void Start()
        {
            var pool = Container.Resolve<IBulletPool>();
            await pool.WarmUpAsync(bulletWarmUpCount);
        }
    }
}