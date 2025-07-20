using Content.Features.LoadingScreenService;
using Core.PrefabFactory;
using Core.SceneLoadingCoordinator;
using Core.UniTaskSceneLoader;
using Zenject;

namespace Content.Global.Scripts
{
    public class GlobalInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            // Prefab Factory
            Container.Bind<IPrefabFactory>().To<PrefabFactory>().AsSingle();

            // Scene Loader
            Container.Bind<ISceneLoader>().To<SceneLoader>().AsSingle();

            // Loading Screen Service
            Container.Bind<ILoadingScreenService>().To<LoadingScreenService>().AsSingle();

            // Scene Loading Coordinator
            Container.Bind<SceneLoadingCoordinator>().AsSingle();

            // TODO: InputService, GameStateService, и т.д.
        }
    }
}