using UnityEngine;
using UnityEngine.UI;
using Zenject;
using Foundation;

namespace Experiments
{
    public sealed class LoopingTrigger : MonoBehaviour
    {
        [Inject] ISoundManager soundManager = default;
        public AudioClip Sound;
        public Transform Target;
        SoundHandle handle;

        void Awake()
        {
            var toggle = GetComponent<Toggle>();
            toggle.isOn = false;
            toggle.onValueChanged.AddListener((value) => {
                    if (value)
                        handle = soundManager.Sfx.PlayAt(Target, Sound, loop: true);
                    else
                        handle.Stop();
                });
        }
    }
}
