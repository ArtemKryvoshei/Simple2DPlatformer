using Core.Other;
using UnityEngine;

namespace Content.Features.LevelProgressService.Scripts
{
    public class LevelProgressService : ILevelProgressService
    {
        public int CurrentLevelIndex => PlayerPrefs.GetInt(ConstantsHolder.LEVEL_INDEX_KEY, 0);

        public void AdvanceToNextLevel()
        {
            int newIndex = CurrentLevelIndex + 1;
            PlayerPrefs.SetInt(ConstantsHolder.LEVEL_INDEX_KEY, newIndex);
            PlayerPrefs.Save();
        }
    }
}