using AddressablesGenerated;
using Core.PrefabFactory;
using UnityEngine;
using Cysharp.Threading.Tasks;

namespace Content.Features.LoadingScreenService
{
    public class LoadingScreenService : ILoadingScreenService
    {
        private readonly IPrefabFactory _factory;
        private readonly string _loadingScreenAddress;
        private GameObject _instance;

        public LoadingScreenService(IPrefabFactory factory)
        {
            _factory = factory;
            _loadingScreenAddress = Address.UI.LoadingScreen; 
        }

        public async UniTask ShowAsync()
        {
            if (_instance == null)
            {
                _instance = await _factory.CreateAsync(_loadingScreenAddress);
                GameObject.DontDestroyOnLoad(_instance);
            }

            _instance.SetActive(true);
        }

        public void Hide()
        {
            if (_instance != null)
            {
                _instance.SetActive(false);
            }
        }
    }
}