using UnityEngine;

namespace Otus
{
    public sealed class SoundManager : MonoBehaviour
    {
        private static SoundManager instance;

        [SerializeField]
        private AudioSource audioSource;
        
        private void Awake()
        {
            instance = this;
            DontDestroyOnLoad(this.gameObject);
        }

        public void PlaySound(AudioClip clip)
        {
            this.audioSource.PlayOneShot(clip);
        }
    }
}