using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    private AsyncLoading asyncTutorialScene;
    private AsyncLoading asyncHubWorldScene;

    public Toggle optionsToggle;
    public GameObject mainGrid;
    public GameObject options;
    public GameObject credits;
    public bool playTutorial = true;

    private void Awake()
    {
        if (AsyncToggle.loadAsynchronously)
        {
            asyncTutorialScene = this.gameObject.AddComponent<AsyncLoading>();
            asyncTutorialScene.sceneToLoadNext = Scenes.TutorialScene;

            asyncHubWorldScene = this.gameObject.AddComponent<AsyncLoading>();
            asyncHubWorldScene.sceneToLoadNext = Scenes.HubWorldScene;
        }

        ShowOptions(false);
        ShowCredits(false);
    }

    public void TutorialToggle()
    {
        playTutorial = optionsToggle.isOn;
    }

    public void PlayGame()
    {
        if (playTutorial)
        {
            if (AsyncToggle.loadAsynchronously)
            {
                asyncTutorialScene.LoadScene();
                System.Threading.Thread.Sleep(100);
                asyncHubWorldScene.KillScene();
            }
            else { SceneTransitionManager.ChangeScenes(Scenes.TutorialScene); }
        }
        else
        {
            if (AsyncToggle.loadAsynchronously)
            {
                asyncTutorialScene.KillScene();
                asyncHubWorldScene.LoadScene();
            }
            else { SceneTransitionManager.ChangeScenes(Scenes.HubWorldScene); }
        }
    }

    public void ShowOptions(bool toggle)
    {
        mainGrid.SetActive(!toggle);
        options.SetActive(toggle);
    }

    public void ShowCredits(bool toggle)
    {
        mainGrid.SetActive(!toggle);
        credits.SetActive(toggle);
    }
}
