using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using YG;

public class YandexFullScreenAdInvoker : MonoBehaviour
{
    [SerializeField] private List<int> ignoreScenesIDs;
    private void OnEnable()
    {
        SceneManager.sceneLoaded += SceneTransitionOnStartLoadScene;
    }
    
    private void OnDisable()
    {
        SceneManager.sceneLoaded -= SceneTransitionOnStartLoadScene;
    }

    private void SceneTransitionOnStartLoadScene(Scene scene, LoadSceneMode loadSceneMode)
    {
        if (ignoreScenesIDs.Contains(scene.buildIndex))
            return;
        YandexGame.FullscreenShow();
    }
}
