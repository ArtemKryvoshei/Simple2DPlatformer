namespace Content.Features.LevelProgressService.Scripts
{
    public interface ILevelProgressService
    {
        int CurrentLevelIndex { get; }
        void AdvanceToNextLevel();
    }
}