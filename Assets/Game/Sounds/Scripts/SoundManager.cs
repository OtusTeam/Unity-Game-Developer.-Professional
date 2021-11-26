using UnityEngine;

namespace Otus
{
    public sealed class SoundManager : MonoBehaviour
    {
        [SerializeField]
        private AudioSource audioSource;
        
        public void PlaySound(AudioClip clip)
        {
            this.audioSource.PlayOneShot(clip);
        }
    }
}