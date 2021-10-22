
using Foundation;
using UnityEngine;
using Zenject;

public class TimeScaleDebug : MonoBehaviour
{
    public float scale = 0.5f;
    [Inject] ITimeScaleManager timeScaleManager = default;
    TimeScaleHandle timeScaleHandle;

    void Awake()
    {
        enabled = false;
    }

    void OnEnable()
    {
        if (timeScaleHandle == null)
            timeScaleHandle = new TimeScaleHandle();
        timeScaleManager.BeginTimeScale(timeScaleHandle, scale);
    }

    void OnDisable()
    {
        timeScaleManager.EndTimeScale(timeScaleHandle);
    }
}
