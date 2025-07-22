using Content.Features.AmmoSystem;
using Content.Features.ConfigsSystem.Scripts;
using UnityEngine;
using Zenject;

namespace Content.Global.Scripts
{
    public class GameplayInstaller : MonoInstaller
    {
        [SerializeField] private PlayerConfig _playerConfig;
        
        public override void InstallBindings()
        {
            Container.Bind<PlayerConfig>().To<PlayerConfig>().FromInstance(_playerConfig).AsSingle();
            Container.Bind<IAmmoSystem>().To<AmmoSystem>().AsSingle();
        }
    }
}