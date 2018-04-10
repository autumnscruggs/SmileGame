using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class LevelConfirmation : MonoBehaviour
{
    public GameObject confirmation;
    public GameObject buttons;
    public Text confirmationText;

    public string levelName;
    public string levelString;

    public bool debugTestLastLevels = false;

    void Start ()
    {
        ToggleConfirmation(false);
    }
	
    public void ConfirmYes()
    {
        if (AsyncToggle.loadAsynchronously) { HubWorldAsync.Instance.LoadScene(levelString); }
        else { SceneTransitionManager.ChangeScenes(levelString); }
    }

    public void ConfirmNo()
    {
        ToggleConfirmation(false);
    }

    public void ToggleConfirmation(bool toggle)
    {
        if(levelString != Scenes.BossScene && levelString != Scenes.Level4Scene)
        {
            confirmationText.text = "Do you want to play " + "\"" + levelName + "\"" + "?";
            buttons.SetActive(true);
        }
        else if (levelString == Scenes.Level4Scene)
        {
            if (LevelManager.LevelsComplete.Count < 3 && !debugTestLastLevels)
            {
                confirmationText.text = "You need to beat the other 3 levels before you can play this level!";
                buttons.SetActive(false);
            }
            else
            {
                confirmationText.text = "Do you want to take on " + "\"" + levelName + "\"" + "?";
                buttons.SetActive(true);
            }
        }
        else
        {
            if (!LevelManager.CanGoToBoss && !debugTestLastLevels)
            {
                confirmationText.text = "You need to beat all the levels before you can take on the boss!";
                buttons.SetActive(false);
            }
            else
            {
                confirmationText.text = "Do you want to take on the boss?";
                buttons.SetActive(true);
            }
        }

        confirmation.SetActive(toggle);
    }
}
