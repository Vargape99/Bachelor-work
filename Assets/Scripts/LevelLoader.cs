using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

//if a scene would have a lot of gameobjects loading have to be done
public class LevelLoader : MonoBehaviour
{
    public void LoadNextLevel() {
        int stage = SceneManager.GetActiveScene().buildIndex + 1;
        if (stage == SceneManager.sceneCountInBuildSettings) {
            stage = 0;
        }
        StartCoroutine(LoadingLevel(stage));
    }

    public void LoadLevelByNumber(int number) {
        StartCoroutine(LoadingLevel(number));
    }

    public int ThisLevel() {
        return SceneManager.GetActiveScene().buildIndex;
    }

    private IEnumerator LoadingLevel(int stage) {
        AsyncOperation loading = SceneManager.LoadSceneAsync(stage);
        while (!loading.isDone) {
            Debug.Log(loading.progress);
            yield return null;
        }
    }
}
