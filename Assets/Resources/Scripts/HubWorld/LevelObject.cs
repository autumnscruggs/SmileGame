using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class LevelObject : MonoBehaviour
{
    private LevelConfirmation confirmation;
    public Text levelText;
    public Scenes.Levels levelToTransitionTo;
    private string levelString;
    public string levelName;
    private bool levelComplete;

    void Awake()
    {
        confirmation = GameObject.FindObjectOfType<LevelConfirmation>();
        levelString = Scenes.TurnLevelIntoSceneName(levelToTransitionTo);
        levelComplete = false;
        levelText.text = levelName;
    }

    void Start()
    {
        if (LevelManager.LevelsComplete.Contains(levelName))
        {
            levelText.text = "Level Complete";
            levelComplete = true;
        }
    }

	void OnTriggerEnter2D(Collider2D collider)
    {
        if(collider.GetComponent<Player>() != null && !levelComplete)
        {
            confirmation.levelName = levelName;
            confirmation.levelString = levelString;
            confirmation.ToggleConfirmation(true);
        }
    }

    void OnTriggerExit2D(Collider2D collider)
    {
        if (collider.GetComponent<Player>() != null && !levelComplete)
        {
            confirmation.ToggleConfirmation(false);
        }
    }
}
