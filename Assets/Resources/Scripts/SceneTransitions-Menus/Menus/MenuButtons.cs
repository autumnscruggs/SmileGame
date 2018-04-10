using UnityEngine;
using System.Collections;

public class MenuButtons : MonoBehaviour
{
    public void ReturnToMenu()
    {
        SceneTransitionManager.ChangeScenes(Scenes.MainMenuScene);
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
