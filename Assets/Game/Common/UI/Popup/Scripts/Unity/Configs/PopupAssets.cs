using System;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Prototype.Unity
{
    [CreateAssetMenu(
        fileName = "PopupAssets",
        menuName = "UI/New PopupAssets"
    )]
    public sealed class PopupAssets : ScriptableObject, ISerializationCallbackReceiver
    {
        [SerializeField]
        private string resourcePath;
        
        [Space]
        [SerializeField]
        private PopupInfo[] popupInfos = new PopupInfo[0];

        private Dictionary<PopupName, string> prefabPathMap;
        
        public Popup LoadPrefab(PopupName name)
        {
            var prefabPath = this.prefabPathMap[name];
            var prefab = Resources.Load<Popup>(prefabPath);
            return prefab;
        }
        
        void ISerializationCallbackReceiver.OnAfterDeserialize()
        {
            var count = this.popupInfos.Length;
            this.prefabPathMap = new Dictionary<PopupName, string>(count);
            for (var i = 0; i < count; i++)
            {
                var info = this.popupInfos[i];
                var popupId = info.popupName;
                this.prefabPathMap[popupId] = this.resourcePath + info.prefabId;
            }
        }

        void ISerializationCallbackReceiver.OnBeforeSerialize()
        {
        }

#if UNITY_EDITOR
        private void OnValidate()
        {
            var count = this.popupInfos.Length;
            for (var i = 0; i < count; i++)
            {
                var info = this.popupInfos[i];
                ProcessPopupInfo(info);
            }
        }

        private static void ProcessPopupInfo(PopupInfo info)
        {
            if (info.initialized)
            {
                return;
            }

            var prefab = info.popupPrefab;
            if (prefab == null)
            {
                return;
            }

            info.prefabId = prefab.name;
            info.popupPrefab = null;
            info.initialized = true;
        }
#endif

        [Serializable]
        private sealed class PopupInfo
        {
            [SerializeField]
            public PopupName popupName;
            
#if UNITY_EDITOR
            [SerializeField]
            public bool initialized;

            [HideIf("initialized")]
            [SerializeField]
            public Popup popupPrefab;
#endif
            [ReadOnly]
            [SerializeField]
            public string prefabId;
        }
    }
}