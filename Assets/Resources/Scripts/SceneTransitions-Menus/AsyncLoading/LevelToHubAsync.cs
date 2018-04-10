using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class LevelToHubAsync : MonoBehaviour
{
    private static LevelToHubAsync singleton; // Singleton instance   
    public static LevelToHubAsync Instance
    {
        get
        {
            if (singleton == null)
            {
                Debug.LogError("[LevelToHubAsync]: Instance does not exist!");
                return null;
            }

            return singleton;
        }
    }

    public AsyncLoading hubWorldAsync;
    public AsyncLoading endGameAsync;
    public AsyncLoading mainMenuAsync;

    void Awake()
    {
        #region Singleton
        // Create singleton instance
        singleton = this;
        #endregion

        if (AsyncToggle.loadAsynchronously)
        {
            hubWorldAsync = this.gameObject.AddComponent<AsyncLoading>();
            hubWorldAsync.sceneToLoadNext = Scenes.HubWorldScene;

            endGameAsync = this.gameObject.AddComponent<AsyncLoading>();
            endGameAsync.sceneToLoadNext = Scenes.EndGameScene;

            mainMenuAsync = this.gameObject.AddComponent<AsyncLoading>();
            mainMenuAsync.sceneToLoadNext = Scenes.MainMenuScene;
        }
    }

    public void LoadHubWorld()
    {
        hubWorldAsync.LoadScene();
        endGameAsync.KillScene();
        mainMenuAsync.KillScene();
    }

    public void LoadQuitScene()
    {
        hubWorldAsync.KillScene();
        endGameAsync.LoadScene();
        mainMenuAsync.KillScene();
    }

    public void LoadMainMenu()
    {
        hubWorldAsync.KillScene();
        endGameAsync.KillScene();
        mainMenuAsync.LoadScene();
    }
}
