using System;
using UnityEngine;
using Zenject;
using DG.Tweening;

namespace Foundation
{
    //Здесь использован пул Zenject, для этого нужен интерфейс IPoolable
    [RequireComponent(typeof(AudioSource))]
    public sealed class SoundSource : MonoBehaviour, IPoolable<AudioClip, IMemoryPool>, IDisposable
    {
        //Так же нужна фабрика, для работы её нужно зарегистрировать в SoundSourceFactory
        public sealed class Factory : PlaceholderFactory<AudioClip, SoundSource>
        {
        }

        IMemoryPool Pool;
        internal AudioSource AudioSource { get; private set; }
        internal int HandleID { get; private set; }
        [ReadOnly] [SerializeField] internal Transform TargetTransform;
        [ReadOnly] [SerializeField] internal bool SurviveSceneLoad;

        void Awake()
        {
            //Работает в паре с AudioSource, по сути SoundSource -- контроллер AudioSource
            AudioSource = GetComponent<AudioSource>();
            AudioSource.playOnAwake = false;

          #if UNITY_EDITOR
            gameObject.name = "<Free>";
          #endif
        }

        public void Dispose()
        {
            Pool.Despawn(this);
        }

        //Инициализация, аналог OnEnable
        public void OnSpawned(AudioClip clip, IMemoryPool pool)
        {
            Pool = pool;
            AudioSource.clip = clip;

          #if UNITY_EDITOR
            gameObject.name = (clip != null ? clip.name : "<Invalid>");
          #endif
        }

        //Деинициализация, аналог OnDisaable
        public void OnDespawned()
        {
            Pool = null;

          #if UNITY_EDITOR
            gameObject.name = "<Free>";
          #endif

            //Сброс твинов, звука, остановки, цели, инвалидация хендла
            AudioSource.DOKill(false);
            if (AudioSource.isPlaying)
                AudioSource.Stop();

            TargetTransform = null;
            AudioSource.clip = null;
            HandleID++;
        }

        public Tweener DOFade(float endValue, float time)
        {
            return DOTween.To(
                    () => AudioSource.volume,
                    (value) => AudioSource.volume = value,
                    endValue,
                    time)
                .SetOptions(false)
                .SetTarget(this);
        }
    }
}
