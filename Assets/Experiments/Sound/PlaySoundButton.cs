using UnityEngine;
using UnityEngine.UI;
using Zenject;
using Foundation;

namespace Experiments
{
    public sealed class PlaySoundButton : MonoBehaviour
    {
        [Inject] ISoundManager soundManager = default;
        public string ChannelName;
        public AudioClip Sound;

        void Awake()
        {
            GetComponent<Button>().onClick.AddListener(() => soundManager.GetChannel(ChannelName).Play(Sound));
        }
    }
}
