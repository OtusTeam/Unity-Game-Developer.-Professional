using System;

namespace Otus
{
    public interface IWeaponManager
    {
        event Action<IWeapon> OnCurrentWeaponChanged;

        IWeapon CurrentWeapon { get; }

        void SetupCurrentWeapon(string weaponId);
        
        void ChangeCurrentWeapon(string weaponId);

        IWeapon GetWeapon(string weaponId);

        IWeapon[] GetAllWeapons();
    }
}