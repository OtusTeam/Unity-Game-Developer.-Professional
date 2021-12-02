using System.Diagnostics;
using Debug = UnityEngine.Debug;

public static class DebugLogger
{
    [Conditional("UNITY_EDITOR")]
    public static void Info(string message)
    {
        Debug.Log(message);
    }

    [Conditional("UNITY_EDITOR")]
    public static void Warning(string message)
    {
        Debug.LogWarning(message);
    }

    [Conditional("UNITY_EDITOR")]
    public static void Error(string message)
    {
        Debug.LogError(message);
    }
}