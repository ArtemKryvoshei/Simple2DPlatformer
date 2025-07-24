using UnityEngine;
using Zenject;

namespace Content.Global.Scripts
{
    public class BootstrapInstaller : MonoInstaller
    {
        [SerializeField] private Bootstrapper _bootstrapper;

        public override void InstallBindings()
        {
            Container.Bind<Bootstrapper>().FromInstance(_bootstrapper).AsSingle();
        }
    }
}