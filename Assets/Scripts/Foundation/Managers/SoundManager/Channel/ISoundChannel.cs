using UnityEngine;

namespace Foundation
{
    public interface ISoundChannel
    {
        string Name { get; }
        bool Enabled { get; set; }
        float Volume { get; set; }

        //surviveSceneLoad -- позволяеет менять пережить выгрузку сцены
        //PlayAt -- привязаны к источнику
        SoundHandle Play(AudioClip clip, bool loop = false, bool surviveSceneLoad = false, float volume = 1.0f);
        SoundHandle PlayAt(GameObject gameObject, AudioClip clip, bool loop = false, bool surviveSceneLoad = false, float volume = 1.0f);
        SoundHandle PlayAt(Component component, AudioClip clip, bool loop = false, bool surviveSceneLoad = false, float volume = 1.0f);
        SoundHandle PlayAt(Transform transform, AudioClip clip, bool loop = false, bool surviveSceneLoad = false, float volume = 1.0f);

        //SoundHandle -- хендл для остановка
        void Stop(SoundHandle sound);

        //Выключает все звуки, включая/не включая те, что переживают сцену
        void StopAllSounds(bool includingSurviveSceneLoad = true);
    }
}
