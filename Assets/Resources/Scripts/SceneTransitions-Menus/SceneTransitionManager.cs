using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class SceneTransitionManager : MonoBehaviour
{
    //I think that one book would be happy about this -- all the statics

    public static void ReloadCurrentScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public static void ChangeScenes(string sceneToChangeTo)
    {
        if(sceneToChangeTo == Scenes.EndGameScene)
        {
            EndSlateManager.Instance.gameObject.transform.parent = null;
            DontDestroyOnLoad(EndSlateManager.Instance.gameObject);
            Destroy(EndSlateManager.Instance.gameObject, 5f);
        }

        SceneManager.LoadScene(sceneToChangeTo);
        LevelManager.CurrentLevel = SceneManager.GetActiveScene().name;

    }
}

public class Scenes
{
    public const string MainMenuScene = "MainMenu";
    public const string HubWorldScene = "HubWorld";
    public const string EndGameScene = "QuitScene";

    public const string TutorialScene = "TutorialScene";
    public const string Level1Scene = "Level1";
    public const string Level2Scene = "Level2";
    public const string Level3Scene = "Level3";
    public const string Level4Scene = "Level4";
    public const string BossScene = "BossLevel";

    public enum Levels { Level1, Level2, Level3, Level4, Boss }
    public static string TurnLevelIntoSceneName(Levels level)
    {
        switch (level)
        {
            case Levels.Level1:
                return Level1Scene;
                break;
            case Levels.Level2:
                return Level2Scene;
                break;
            case Levels.Level3:
                return Level3Scene;
                break;
            case Levels.Level4:
                return Level4Scene;
                break;
            case Levels.Boss:
                return BossScene;
                break;
        }

        return "";
    }
}
