using Foundation;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

public class SceneLoadButton : MonoBehaviour
{
    public string SceneName;
    [Inject] ISceneManager sceneManager = default;

    void Awake()
    {
        GetComponent<Button>().onClick.AddListener(() => sceneManager.LoadSceneAsync(SceneName));
    }
}
