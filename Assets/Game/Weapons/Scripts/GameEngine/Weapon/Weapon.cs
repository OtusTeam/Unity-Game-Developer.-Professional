using System;
using DynamicObjects;
using UnityEngine;

namespace Otus
{
    [Serializable]
    public sealed class Weapon
    {
        public WeaponConfig Config
        {
            get { return this.config; }
        }

        public MonoDynamicObject DynamicObject
        {
            get { return this.dynamicObject; }
        }

        [SerializeField]
        private WeaponConfig config;

        [SerializeField]
        private MonoDynamicObject dynamicObject;

        public Weapon(WeaponConfig config, MonoDynamicObject dynamicObject)
        {
            this.config = config;
            this.dynamicObject = dynamicObject;
        }
    }
}