using UnityEngine;
using UnityEngine.UI;
using Zenject;
using Foundation;

namespace Experiments
{
    public sealed class PlayMusicButton : MonoBehaviour
    {
        [Inject] ISoundManager soundManager = default;
        public AudioClip Music;

        void Awake()
        {
            GetComponent<Button>().onClick.AddListener(() => soundManager.PlayMusic(Music));
        }
    }
}
