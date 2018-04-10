using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class CombatTutorial : MonoBehaviour
{
    public enum TutorialSequence { Beginning, Smile, NPCTurn, Compliment, NPCTurn2, Items, UsePotion, EndTutorial, None }
    public TutorialSequence tutorialState;
    private TutorialText tutorial;
    public Button[] buttons = new Button[3];

    private ItemGUIButton[] itemButtons;
    private ItemGUIButton potionButton;
    public Button cancelItemsButton;

    public GameObject items;

    private float beginningTime = 6f;
    private float npcTurnExplanationTime = 8f;
    private float buttonExplanationTime = 150f;
    private float npcTurn2ExplanationTime = 8f;

    private bool inThing = false;

    public RectTransform combatGrid;
    public HorizontalLayoutGroup options;

    void Awake()
    {
        tutorial = GameObject.FindObjectOfType<TutorialText>();
    }

    void Start()
    {
        cancelItemsButton.interactable = false;
        buttons[0].onClick.AddListener(SmileButtonTutorial);
        buttons[1].onClick.AddListener(ComplimentButtonTutorial);
        buttons[2].onClick.AddListener(ItemsButtonTutorial);
    }

    void Update()
    {
        if(tutorialState == TutorialSequence.Beginning && tutorial.TimeIsUp)
        {
            tutorialState = TutorialSequence.Smile;
        }
        else if(tutorialState == TutorialSequence.Smile)
        {
            buttons[0].interactable = true;
            tutorial.SetTutorialText("Your first option is Smile! It takes away a small amount of sadness!" +
                " Click it now to see how it works", buttonExplanationTime);
            tutorial.ToggleOkButton(false);
        }
        else if (tutorialState == TutorialSequence.NPCTurn)
        {
            if (!inThing)
            {
                inThing = true;
                tutorial.SetTutorialText("The Sadness is fighting back! It is difficult to help people overcome their inner pain." +
                 " So you'll help, and then it'll fight back! You take turns!", npcTurnExplanationTime);
            }

            if(tutorial.TimeIsUp)
            {
                inThing = false;
                tutorialState = TutorialSequence.Compliment;
            }
        }

        else if (tutorialState == TutorialSequence.Compliment)
        {
            buttons[1].interactable = true;
            tutorial.SetTutorialText("The second way you can combat sadness is Complimenting! Though Compliments are dangerous." +
                " They hurt your happiness a little too! Try it now!", buttonExplanationTime);
            tutorial.ToggleOkButton(false);
        }

        else if (tutorialState == TutorialSequence.NPCTurn2)
        {
            if (!inThing)
            {
                inThing = true;
                tutorial.SetTutorialText("Ah, the Sadness is recovering health! Different people have different types of Sadness." +
                " You've got to watch out for their moves and figure out how to win!", npcTurn2ExplanationTime);
            }
            if(tutorial.TimeIsUp)
            {
                inThing = false;
                tutorialState = TutorialSequence.Items;
            }
        }

        else if (tutorialState == TutorialSequence.Items)
        {
            buttons[2].interactable = true;
            tutorial.SetTutorialText("Your health is getting kind of low, we should use a Potion to bring up our happiness!" +
                " Click on the items to open up that menu!", buttonExplanationTime);
            tutorial.ToggleOkButton(false);
        }

        else if (tutorialState == TutorialSequence.UsePotion)
        {
            potionButton.gameObject.GetComponent<Button>().interactable = true;
            tutorial.SetTutorialText("Click on the Potion button to use it!", buttonExplanationTime);
        }

        else if (tutorialState == TutorialSequence.EndTutorial)
        {
            if (!inThing)
            {
                inThing = true;
                foreach (Button b in buttons) { b.interactable = true; }
                cancelItemsButton.interactable = true;
                itemButtons = GameObject.FindObjectsOfType<ItemGUIButton>();
                foreach (ItemGUIButton b in itemButtons) { b.gameObject.GetComponent<Button>().interactable = true; }
                tutorial.SetTutorialText("Well that's the end of the tutorial! Good luck finishing the battle!", 3f);

                buttons[0].onClick.RemoveListener(SmileButtonTutorial);
                buttons[1].onClick.RemoveListener(ComplimentButtonTutorial);
                buttons[2].onClick.RemoveListener(ItemsButtonTutorial);
            }


            if(tutorial.TimeIsUp)
            {
                options.padding.top = 15;
                options.padding.bottom = 15;
                options.padding.left = 15;
                options.padding.right = 15;
                combatGrid.offsetMax = new Vector2(combatGrid.offsetMax.x, 0);
                tutorialState = TutorialSequence.None;
            }
        }
    }

    public void SmileButtonTutorial()
    {
        buttons[0].interactable = false;
        tutorialState = TutorialSequence.NPCTurn;
        tutorial.StopTimer();
    }

    public void ComplimentButtonTutorial()
    {
        buttons[1].interactable = false;
        tutorialState = TutorialSequence.NPCTurn2;
        tutorial.StopTimer();
    }

    public void ItemsButtonTutorial()
    {
        buttons[2].interactable = false;
        tutorialState = TutorialSequence.UsePotion;
        tutorial.StopTimer();
    }

    public void PotionButtonTutorial()
    {
        tutorialState = TutorialSequence.EndTutorial;
        tutorial.StopTimer();
    }


    public void BeginCombatTutorial()
    {
        tutorial.StopTimer();
        tutorialState = TutorialSequence.Beginning;

        combatGrid.transform.parent.gameObject.SetActive(true);
        items.SetActive(true);

        foreach (Button b in buttons) { b.interactable = false; }
        itemButtons = GameObject.FindObjectsOfType<ItemGUIButton>();
        foreach (ItemGUIButton b in itemButtons) { b.GetComponent<Button>().interactable = false; }

        potionButton = System.Array.Find(itemButtons, item => item.itemToUse.GetType() == typeof(Potion));

        potionButton.gameObject.GetComponent<Button>().onClick.AddListener(PotionButtonTutorial);

        tutorial.SetTutorialText("This is how you help others out, by Battling Sadness! The buttons at the bottom detail what you can do!", beginningTime);

        items.SetActive(false);
    }

    IEnumerator ForceWait()
    {
        yield return new WaitForSeconds(0.5f);

        
    }
}
