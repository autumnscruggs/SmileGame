using UnityEngine;
using System.Collections;

public class EndSlateGUI : MonoBehaviour
{
    public GameObject victory;
    public GameObject gameOver;

    private void Start()
    {
        if(EndSlateManager.Instance != null)
        {
            if(EndSlateManager.Instance.endSlate == EndState.Victory)
            {
                ToggleVictory();
            }
            else
            {
                ToggleGameOver();
            }
        }
    }

    public void ToggleVictory()
    {
        victory.SetActive(true);
        gameOver.SetActive(false);
    }
    public void ToggleGameOver()
    {
        victory.SetActive(false);
        gameOver.SetActive(true);
    }
}
