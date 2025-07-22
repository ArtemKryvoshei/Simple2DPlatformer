namespace Content.Features.AmmoSystem
{
    public interface IAmmoSystem
    {
        bool CanShoot { get; }
        int CurrentAmmo { get; }
        int MaxAmmo { get; }
        void SetMaxAmmo(int maxAmmo);
        bool TryConsumeAmmo();
    }
}