using UnityEngine;
using DG.Tweening;

namespace Foundation
{
    //Это обёртка, нужная для переиспользования SoundSource в пуле
    //По сути, это внешний интерфейс SoundSource, решающий проблему множественного доступа и пула
    //Почти все методы -- обращение к SoundSource и проверка корректности обращения
    public struct SoundHandle
    {
        internal readonly SoundSource Source;

        //Меняется при возвращении в пул, чтобы инвалидировать SoundHandle
        internal readonly int HandleID;

        public readonly ISoundChannel Channel;
        public bool IsValid => (Source != null && Source.HandleID == HandleID);
        public bool IsPlaying => (IsValid ? Source.AudioSource.isPlaying : false);
        public AudioClip AudioClip => (IsValid ? Source.AudioSource.clip : null);

        public float Volume { get {
                DebugOnly.Check(IsValid, "Attempted to get volume with an invalid SoundHandle.");
                return Source.AudioSource.volume;
            } set {
                DebugOnly.Check(IsValid, "Attempted to set volume with an invalid SoundHandle.");
                Source.AudioSource.volume = value;
            } }

        public bool Loop { get {
                DebugOnly.Check(IsValid, "Attempted to get looping with an invalid SoundHandle.");
                return Source.AudioSource.loop;
            } set {
                DebugOnly.Check(IsValid, "Attempted to set looping with an invalid SoundHandle.");
                Source.AudioSource.loop = value;
            } }

        internal SoundHandle(ISoundChannel channel, SoundSource source)
        {
            Channel = channel;
            Source = source;
            HandleID = source.HandleID;
        }

        //Обёркта для удобства
        public void Stop()
        {
            Channel.Stop(this);
        }

        //Анимация громкости
        public Tweener DOFade(float endValue, float time)
        {
            DebugOnly.Check(IsValid, "Attempted to fade volume with an invalid SoundHandle.");
            return Source.DOFade(endValue, time);
        }

        //Анимация громкости и выключение
        public Tweener DOFadeToStop(float time)
        {
            DOKill(false);
            return DOFade(0.0f, time).OnComplete(Stop);
        }

        //Отмена анимации
        public void DOKill(bool complete)
        {
            DebugOnly.Check(IsValid, "Attempted to kill tweens with an invalid SoundHandle.");
            Source.DOKill(complete);
        }
    }
}
