using UnityEngine;
using System.Collections;

public class TutorialTrigger : MonoBehaviour
{
    public string textToShow;
    public float timeToShowText;
    private TutorialText tutorialText;

    void Awake() { tutorialText = GameObject.FindObjectOfType<TutorialText>(); }

    void OnTriggerEnter2D(Collider2D collider)
    {
        if(collider.GetComponent<Player>() != null)
        {
            tutorialText.SetTutorialText(textToShow, timeToShowText);
            tutorialText.ToggleOkButton(false);
        }
    }
}
