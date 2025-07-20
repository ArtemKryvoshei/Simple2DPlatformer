using AddressablesGenerated;
using Core.SceneLoadingCoordinator;
using UnityEngine;
using Zenject;

namespace Content.Global.Scripts
{
    public class Bootstrapper : MonoBehaviour
    {
        [SerializeField] private string _mainMenuSceneName = "MainMenu";
        
        [Inject] private SceneLoadingCoordinator _sceneLoadingCoordinator;
        
        private async void Start()
        {
            await _sceneLoadingCoordinator.LoadSceneWithUI(_mainMenuSceneName);
        }
    }
}