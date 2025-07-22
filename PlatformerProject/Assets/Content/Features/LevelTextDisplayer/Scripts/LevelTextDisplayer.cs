using Content.Features.LevelProgressService.Scripts;
using TMPro;
using UnityEngine;
using Zenject;

namespace Content.Features.LevelTextDisplayer.Scripts
{
    public class LevelTextDisplayer : MonoBehaviour
    {
        [SerializeField] private TMP_Text _text;
        [SerializeField] private string _format = "Level {0}";

        private ILevelProgressService _levelProgressService;

        [Inject]
        public void Construct(ILevelProgressService levelProgressService)
        {
            _levelProgressService = levelProgressService;
        }

        private void Start()
        {
            UpdateText();
        }

        private void UpdateText()
        {
            _text.text = string.Format(_format, _levelProgressService.CurrentLevelIndex + 1); 
        }
    }
}