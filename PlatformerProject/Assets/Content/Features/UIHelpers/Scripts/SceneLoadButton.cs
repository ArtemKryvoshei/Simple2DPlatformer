using Core.SceneLoadingCoordinator;
using Cysharp.Threading.Tasks;
using UnityEngine;
using Zenject;

namespace Content.Features.UIHelpers
{
    public class SceneLoadButton : MonoBehaviour
    {
        [SerializeField] private string _sceneName;
        
        [Inject] private SceneLoadingCoordinator _sceneLoading;

        public async void LoadLevelByString()
        {
            await _sceneLoading.LoadSceneWithUI(_sceneName);
        }
    }
}