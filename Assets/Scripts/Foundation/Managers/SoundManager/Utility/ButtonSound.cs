using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace Foundation
{
    public sealed class ButtonSound : MonoBehaviour
    {
        [Inject] ISoundManager soundManager = default;
        public AudioClip Sound;

        void Awake()
        {
            GetComponent<Button>().onClick.AddListener(() => soundManager.Sfx.Play(Sound));
        }
    }
}
