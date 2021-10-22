using Foundation;
using System;
using UnityEngine;
using Zenject;

namespace Game
{
    public sealed class CharacterWeapon : AbstractService<ICharacterWeapon>, ICharacterWeapon, IOnUpdate
    {
        [Serializable]
        public struct SelectableWeapon
        {
            public Weapon Weapon;
            public string InputActionName;
        }

        [SerializeField] Weapon currentWeapon = Weapon.None;
        public Weapon CurrentWeapon => currentWeapon;

        [SerializeField] SelectableWeapon[] weapons;

        [Inject] IPlayer player = default;
        [Inject] IInputManager inputManager = default;
        [Inject] ISceneState sceneState = default;

        protected override void OnEnable()
        {
            base.OnEnable();
            Observe(sceneState.OnUpdate);
        }

        void IOnUpdate.Do(float timeDelta)
        {
            var input = inputManager.InputForPlayer(player.Index);

            if (weapons != null) {
                foreach (var weapon in weapons) {
                    if (input.Action(weapon.InputActionName).Triggered)
                        currentWeapon = weapon.Weapon;
                }
            }
        }
    }
}
