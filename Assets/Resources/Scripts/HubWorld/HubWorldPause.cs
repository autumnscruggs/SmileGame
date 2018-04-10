using UnityEngine;
using System.Collections;

public class HubWorldPause : MonoBehaviour
{
    public GameObject pauseMenu;
    public KeyCode pauseMenuKey;

    void Awake()
    {
        TogglePauseMenu(false);
    }

    void Update ()
    {
        if (KeyboardInput.IsKeyDown(pauseMenuKey))
        {
            TogglePauseMenu();
        }
    }

    public void YesButton()
    {
        SceneTransitionManager.ChangeScenes(Scenes.MainMenuScene);
    }

    public void NoButton()
    {
        TogglePauseMenu(false);
    }

    private void TogglePauseMenu(bool toggle)
    {
        pauseMenu.SetActive(toggle);
        PauseGame(toggle);
    }

    private void TogglePauseMenu()
    {
        pauseMenu.SetActive(!pauseMenu.activeInHierarchy);
        PauseGame(pauseMenu.activeInHierarchy);
    }

    private void PauseGame(bool paused)
    {
        if (paused)
        {
            GameObject.FindObjectOfType<PlayerController>().enabled = false;
        }
        else
        {
            GameObject.FindObjectOfType<PlayerController>().enabled = true;
        }
    }
}
