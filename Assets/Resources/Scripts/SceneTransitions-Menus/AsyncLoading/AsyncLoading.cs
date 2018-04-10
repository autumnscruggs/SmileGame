using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class AsyncLoading : MonoBehaviour
{
    private string previousScene;
    public string sceneToLoadNext = "";
    public AsyncOperation asyncOperation;

    private bool killScene = true;

    private void Start()
    {
        //Debug.Log(sceneToLoadNext + " // " + "Dying? - " + killScene);
        previousScene = SceneManager.GetActiveScene().name;
        StartCoroutine(LoadGameScene());
    }

    IEnumerator LoadGameScene()
    {
        asyncOperation = SceneManager.LoadSceneAsync(sceneToLoadNext, LoadSceneMode.Additive);
        asyncOperation.allowSceneActivation = false;

        while (!asyncOperation.isDone)
        {
            //Debug.Log("progress: " + asyncOperation.progress);
            yield return new WaitForEndOfFrame();
        }

        yield return new WaitForEndOfFrame();

        Debug.Log(sceneToLoadNext + " // " + "Dying? - " + killScene);

        if (killScene)
        {
            bool b = SceneManager.UnloadScene(SceneManager.GetSceneByName(sceneToLoadNext));
            Debug.Log("This Scene Unloading - " + b);
        }
        else if(!killScene)
        {
            SceneManager.SetActiveScene(SceneManager.GetSceneByName(sceneToLoadNext));
            Debug.Log("Loading Scene");
            KillPreviousScene();
        }
    }

    public void LoadScene()
    {
        killScene = false;
        asyncOperation.allowSceneActivation = true;
    }

    private void KillPreviousScene()
    {
        bool b = SceneManager.UnloadScene(SceneManager.GetSceneByName(previousScene));
        //Debug.Log("Previous Scene Unloading - " + b);
    }

    public void KillScene()
    {
        asyncOperation.allowSceneActivation = true;
    }

}
