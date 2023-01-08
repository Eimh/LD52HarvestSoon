using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    private static SceneLoader instance;
    private bool canLoad;

    void Awake() {
        if (instance != null && instance != this) {
            Destroy(gameObject);
        }
        else {
            instance = this;
        }
    }

    public static void StartLoadScene(string scene) {
        instance.StartCoroutine(instance.LoadSceneAsync(scene));
    }

    public static void CompleteLoadScene() {
        instance.canLoad = true;
    }

    public IEnumerator LoadSceneAsync(string scene) {
        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync("Scenes/" + scene, LoadSceneMode.Single);
        asyncLoad.allowSceneActivation = false;
        while (!asyncLoad.isDone) {
            if (canLoad) {
                asyncLoad.allowSceneActivation = true;
                canLoad = false;
            }
            yield return null;
        }
    }
}
