using DynamicObjects;
using UnityEngine;

namespace Otus
{
    [CreateAssetMenu(
        fileName = "WeaponConfig",
        menuName = "Weapons/New WeaponConfig"
    )]
    public class WeaponConfig : ScriptableObject
    {
        [SerializeField]
        public string id;

        [SerializeField]
        public KeyCode selectActionKey;

        [SerializeField]
        public MonoDynamicObject prefab;
    }
}