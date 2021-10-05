using UnityEngine;
using System.Collections.Generic;

namespace Foundation
{
    public sealed class TimeScaleManager : AbstractManager<ITimeScaleManager>, ITimeScaleManager
    {
        List<TimeScaleHandle> handles = new List<TimeScaleHandle>();

        void Awake()
        {
            UpdateTimeScale();
        }

        void UpdateTimeScale()
        {
            float scale = 1.0f;
            foreach (var handle in handles)
                scale *= handle.Scale;
            Time.timeScale = scale;
        }

        public TimeScaleHandle BeginTimeScale(float scale)
        {
            var handle = new TimeScaleHandle(scale);
            handles.Add(handle);
            UpdateTimeScale();
            return handle;
        }

        public void EndTimeScale(TimeScaleHandle handle)
        {
            handles.Remove(handle);
            UpdateTimeScale();
        }
    }
}
