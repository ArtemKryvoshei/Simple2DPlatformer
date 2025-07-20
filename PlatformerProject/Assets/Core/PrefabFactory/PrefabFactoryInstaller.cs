using Zenject;


namespace Core.PrefabFactory
{
    public class PrefabFactoryInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            Container.Bind<IPrefabFactory>().To<PrefabFactory>().AsSingle();
        }
    }
}