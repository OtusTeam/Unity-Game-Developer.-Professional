using Foundation;
using UnityEngine;
using Zenject;

namespace Game
{
    public interface ICharacterWeapon
    {
        Weapon CurrentWeapon { get; }
    }
}
