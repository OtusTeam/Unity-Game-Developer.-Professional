using UnityEngine;
using UnityEngine.UI;
using Zenject;
using TMPro;

namespace Foundation
{
    [RequireComponent(typeof(TextMeshProUGUI))]
    [ExecuteAlways]
    public sealed class TextLocalizer : AbstractBehaviour, IOnLanguageChanged
    {
        public LocalizedString StringID;

        [Inject] ILocalizationManager locaManager = default;
        TextMeshProUGUI text;

        void Awake()
        {
            text = GetComponent<TextMeshProUGUI>();
        }

        //Подписка на смену языка и установка текущего
        protected override void OnEnable()
        {
            if (Application.IsPlaying(this))
            {
                Observe(locaManager.OnLanguageChanged);
                text.text = locaManager.GetString(StringID);
            }
        }

        void IOnLanguageChanged.Do()
        {
            text.text = locaManager.GetString(StringID);
        }

        //Позволяет переключать язык без запуска (ExecuteAlways)
        #if UNITY_EDITOR
        void Update()
        {
            if (Application.IsPlaying(this))
                return;

            if (text == null)
                text = GetComponent<TextMeshProUGUI>();
            if (text != null)
                text.text = LocalizationData.EditorGetLocalization(StringID);
        }
        #endif
    }
}
