using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class DotweenDelay : MonoBehaviour
{
    public float delay;//

    public void DoSomething()
    {
        Debug.Log("Before delay");
        DOVirtual.DelayedCall(delay, () => Debug.Log("Doing something after delay"));
    }
}
