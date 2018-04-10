using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class TutorialText : MonoBehaviour
{
    private PlayerController controller;
    public GameObject tutorialHolder;
    public Text tutText;
    private float tutorialTimer;
    private float tutTime;
    private bool startTimer = false;

    [SerializeField] private GameObject okButton;

    private bool initialWaitTimePassed = false;

    public float Timer { get { return tutorialTimer; } }
    public bool TimeIsUp { get; private set; }

    void Awake() { controller = GameObject.FindObjectOfType<PlayerController>(); }

    void Start()
    {
        controller.enabled = false;
        SetTutorialText("Hey! This is the tutorial! Use WASD to walk over and look at stuff." +
            "\nPress ESC to pause and/or skip the tutorial!", 4f);
    }

    void Update()
    {
        if(!initialWaitTimePassed && TimeIsUp) { controller.enabled = true; initialWaitTimePassed = true; }

        if (startTimer)
        {
            tutorialTimer += Time.deltaTime;
            if (tutorialTimer >= tutTime)
            {
                StopTimer();
            }
        }
    }

    public void ToggleOkButton(bool toggle) { okButton.SetActive(toggle); }

    public void SetTutorialText(string text, float time)
    {
        StopTimer();
        tutText.text = text;
        StartTutorialTimer(time);
    }

    private void StartTutorialTimer(float time)
    {
        tutTime = time;
        startTimer = true;
        tutorialHolder.SetActive(true);
        TimeIsUp = false;
    }

    public void StopTimer()
    {
        TimeIsUp = true;
        startTimer = false;
        ToggleOkButton(true);
        tutorialHolder.SetActive(false);
        tutorialTimer = 0;
    }

}
