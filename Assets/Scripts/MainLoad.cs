using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MainLoad : MonoBehaviour
{
    public Slider loadingBar;
    bool isLoading;

    void Start()
    {
        loadingBar.value = 0;
        isLoading = false;
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0) && isLoading == false)
        {
            isLoading = true;
            StartCoroutine("LoadGame");
        }
    }

    IEnumerator LoadGame()
    {
        AsyncOperation async = SceneManager.LoadSceneAsync("Game");
        async.allowSceneActivation = false;

        while (!async.isDone)
        {
            loadingBar.value = async.progress;
            yield return true;

            async.allowSceneActivation = true;
        }
    }
}
