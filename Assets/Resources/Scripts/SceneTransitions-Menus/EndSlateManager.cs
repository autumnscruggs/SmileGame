using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public enum EndState { Victory, GameOver }

public class EndSlateManager : MonoBehaviour
{
    private static EndSlateManager singleton; // Singleton instance   
    public static EndSlateManager Instance
    {
        get
        {
            if (singleton == null)
            {
                Debug.LogError("[EndSlateManager]: Instance does not exist!");
                return null;
            }

            return singleton;
        }
    }

    public EndState endSlate;

    void Awake()
    {
        #region Singleton
        // Create singleton instance
        singleton = this;
        #endregion
    }

    private void OnEnable()
    {
        SceneManager.sceneLoaded += OnLevelFinishedLoading;
    }

    private void OnDisable()
    {
        SceneManager.sceneLoaded -= OnLevelFinishedLoading;
    }

    void OnLevelFinishedLoading(Scene scene, LoadSceneMode mode)
    {
        if (scene.ToString() == Scenes.EndGameScene)
        {
            switch (endSlate)
            {
                case EndState.Victory:
                    GameObject.FindObjectOfType<EndSlateGUI>().ToggleVictory();
                    break;
                case EndState.GameOver:
                    GameObject.FindObjectOfType<EndSlateGUI>().ToggleGameOver();
                    break;
            }
        }
    }

    public void GameOver()
    {
        endSlate = EndState.GameOver;
        if (AsyncToggle.loadAsynchronously) { LevelToHubAsync.Instance.LoadQuitScene(); }
        else { SceneTransitionManager.ChangeScenes(Scenes.EndGameScene); }
        MusicManager.Instance.ChangeMusicState(MusicState.Defeat, false, true);
    }

    public void Victory()
    {
        endSlate = EndState.Victory;
        if (AsyncToggle.loadAsynchronously) { LevelToHubAsync.Instance.LoadQuitScene(); }
        else { SceneTransitionManager.ChangeScenes(Scenes.EndGameScene); }
        MusicManager.Instance.ChangeMusicState(MusicState.Victory, false, true);
    }
}