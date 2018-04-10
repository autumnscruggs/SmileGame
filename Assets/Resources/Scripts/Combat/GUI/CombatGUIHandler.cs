using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;
using Observer;
using System;
using Commands;

public class CombatGUIHandler : MonoBehaviour, IObserver
{
    public NPCLoseAnimation nPCLoseAnimation;
    private bool npcLost;
    private bool playNPCAnimation;
    private float timeUntilAnimation;

    public GameObject combatPanel;

    public Slider playerHP;
    public Slider enemySP;
    public Image playerImage;
    public Image enemyImage;
    public Text statusText;
    public Text healthChangeText;
    public GameObject buttonsGrid;

    private Player player;
    private NPC npc;

    private float lastPlayerHealth; //should be in combat manager? though all it's used for is printing
    private float lastNPCHealth; //so I have no idea if these belong here
    private bool gotHealthValues;

    private CombatManager manager;
    private CommandProcessor commandProcessor;
    private ItemGUI itemGUI;

    private int animationTime = 3;

    private Color originalColor;
    private Color fadedColor;
    private Color gainColor;

    public GameObject inactiveButton;

    private bool startTimer = false;
    public float inactivityTimer = 0;
    private int inactiveTime = 5;

    void Awake()
    {
        originalColor = Color.white;
        combatPanel.SetActive(false);
        playNPCAnimation = true;
        inactiveButton.SetActive(false);
        inactiveButton.GetComponent<Button>().onClick.AddListener(BrokenButton);
    }

    void Start()
    {
        itemGUI = GameObject.FindObjectOfType<ItemGUI>();
        commandProcessor = GameObject.FindObjectOfType<CommandProcessor>();
        manager = this.GetComponent<CombatManager>();
        manager.Attach(this);
    }

    private void InactivityTimer()
    {
        inactivityTimer += Time.deltaTime;
        if(inactivityTimer > inactiveTime)
        {
            inactiveButton.SetActive(true);
            startTimer = false;
        }
    }

    public void BrokenButton()
    {
        manager.ResetTurns();
    }

    private void ResetTimer()
    {
        inactivityTimer = 0;
        inactiveButton.SetActive(false);
    }

    void Update()
    {
        if (startTimer) { InactivityTimer(); }
    }

    private void FindPlayer()
    {
        bool playerFound = manager.participants.Find(x => x.GetComponent<Player>() != null);
        if (playerFound)
        { player = manager.participants.Find(x => x.GetComponent<Player>() != null) as Player; }
        else { new Exception("PLAYER WAS NOT FOUND IN PARTICIPANTS -- PLAYER IS REQUIRED FOR COMBAT GUI"); }
    }

    private void FindNPC()
    {
        bool npcFound = manager.participants.Find(x => x.GetComponent<NPC>() != null);
        if (npcFound)
        { npc = manager.participants.Find(x => x.GetComponent<NPC>() != null) as NPC; }
        else { new Exception("NPC WAS NOT FOUND IN PARTICIPANTS -- NPC IS REQUIRED FOR COMBAT GUI"); }
    }

    private void SetUpCombatGUI()
    {
        FindPlayer();
        FindNPC();
        SetSliderValues();

        if (player != null && npc != null)
        {
            UpdateGUI();
            playerImage.sprite = player.GetComponent<SpriteRenderer>().sprite;
            enemyImage.sprite = npc.GetComponent<SpriteRenderer>().sprite;
        }
        statusText.text = "Battle has begun!";
    }

    private void SetSliderValues()
    {
        playerHP.maxValue = player.MaxHealth;
        enemySP.maxValue = npc.MaxHealth;
    }

    private void UpdateGUI()
    {
        if (player != null && npc != null)
        {
            enemySP.value = npc.Health;
            playerHP.value = player.Health;
        }
    }

    private void UpdateGUI(string newStatus)
    {
        UpdateGUI();
        statusText.text = newStatus;
    }

    private void HealthChangeText(string text)
    {
        healthChangeText.text = text;
    }

    public void ObserverUpdate(object sender, object message)
    {
        if (sender is CombatManager)
        {
            if (message is Turn)
            {
                switch ((Turn)message)
                {
                    case Turn.Player:
                        startTimer = false;
                        if (manager.playerCanMove) { buttonsGrid.SetActive(true); }
                        else { PlayerCantMove(); }
                        break;
                    case Turn.NPC:
                        startTimer = false;
                        buttonsGrid.SetActive(false);
                        ResetTimer();
                        startTimer = true;
                        break;
                }
            }

            if (message is BattleState)
            {
                switch ((BattleState)message)
                {
                    case BattleState.BattleBegins:
                        ResetTimer();
                        gotHealthValues = false;
                        HealthChangeText("");
                        combatPanel.SetActive(true);
                        buttonsGrid.SetActive(true);
                        SetUpCombatGUI();
                        GetLastHealth();
                        break;
                    case BattleState.NextTurn:
                        //start next turn
                        break;
                    case BattleState.TurnOver:
                        AddHealthDifferenceText();
                        System.Threading.Thread.Sleep(10); //all the hacks to make sure there's some wait time here
                        GetLastHealth();
                        break;
                    case BattleState.Victory:
                        if (manager.deadParticipant is NPC) { npcLost = true; }
                        else { npcLost = false; }
                        buttonsGrid.SetActive(false);
                        break;
                    case BattleState.BattleEnds:
                        combatPanel.SetActive(false);

                        #region Play Animation the first time
                        if (playNPCAnimation && npcLost)
                        {
                            nPCLoseAnimation.StartAnimation();
                            playNPCAnimation = false;
                        }
                        #endregion
                        break;
                }
            }

            UpdateGUI(commandProcessor.lastCommand.commandName);
        }
    }

    private void GetLastHealth()
    {
        if (!gotHealthValues)
        {
            lastPlayerHealth = player.Health;
            lastNPCHealth = npc.Health;
            gotHealthValues = true;
        }
    }

    private void GainedHealthFlash(Image image)
    {
        originalColor = image.color;
        gainColor = Color.green;

        StartCoroutine(GainImageFade(image));
    }

    private void LostHealthFlash(Image image)
    {
        originalColor = image.color;
        fadedColor = image.color;
        fadedColor.a = 0;

        StartCoroutine(HurtImageFlash(image));
    }

    private void AddHealthDifferenceText()
    {
        Reset();

        string newPlayerText = "";
        string newNPCText = "";

        //Debug.Log("Prev Player Health: " + lastPlayerHealth + " // " + "Current Player Health: " + player.Health);

        if (player.Health != lastPlayerHealth)
        {
            if (player.Health > lastPlayerHealth)
            {
                double healthDiff = System.Math.Ceiling(player.Health - lastPlayerHealth);
                newPlayerText = "Player gained " + healthDiff + " HP";
                GainedHealthFlash(playerImage);
            }
            else if (player.Health < lastPlayerHealth)
            {
                double healthDiff = System.Math.Ceiling(lastPlayerHealth - player.Health);
                newPlayerText = "Player lost " + healthDiff + " HP";
                LostHealthFlash(playerImage);
            }

            gotHealthValues = false;
        }
        if (npc.Health != lastNPCHealth)
        {
            //Debug.Log("Prev NPC Health: " + lastNPCHealth + " // " + "Current NPC Health: " + npc.Health);

            if (npc.Health > lastNPCHealth)
            {
                double healthDiff = System.Math.Ceiling(npc.Health - lastNPCHealth);
                newNPCText = "NPC gained " + healthDiff + " SP";
                GainedHealthFlash(enemyImage);
            }
            else if (npc.Health < lastPlayerHealth)
            {
                double healthDiff = System.Math.Ceiling(lastNPCHealth - npc.Health);
                newNPCText = "NPC lost " + healthDiff + " SP";
                LostHealthFlash(enemyImage);
            }

            gotHealthValues = false;
        }

        HealthChangeText(newPlayerText + "\n" + newNPCText);
    }

    #region Command Buttons

    public void SmileButton()
    {
        CommandCreator.God.ExecuteSmile(player.gameObject);
    }

    public void ComplimentButton()
    {
        CommandCreator.God.ExecuteCompliment(player.gameObject);
    }

    public void PlayerCantMove()
    {
        CommandCreator.God.ExecuteSkipTurn(player.gameObject, "Player can't move!");
    }

    public void ItemButton()
    {
        itemGUI.ToggleCombatItemGUI(true);
    }

    #endregion

    private void Reset()
    {
        StopAllCoroutines();
        playerImage.color = originalColor;
        enemyImage.color = originalColor;
    }

    //I know Coroutines are bad but they're just a quick GUI aesthetics

    IEnumerator HurtImageFlash(Image image)
    {
        for(float x = 0; x < 1; x += 0.1f)
        {
            //Debug.Log(image.gameObject.name + "// " + " In hurt flash");
            image.color = originalColor;
            yield return new WaitForSeconds(0.01f);
            image.color = fadedColor;
        }

        yield return new WaitForSeconds(0.05f);
        image.color = originalColor;
    }

    IEnumerator GainImageFade(Image image)
    {
        Color originalColorCopy = originalColor;
        originalColorCopy.a = 0;

        for (float x = 0; x < 0.2f; x += 0.01f)
        {
            Color a = Color.Lerp(originalColor, gainColor, x);
            //Debug.Log(image.gameObject.name + "// " + " In gain fade #1");
            image.color = a;
            yield return new WaitForSeconds(0.01f);
        }

        for (float x = 0; x < 0.2f; x += 0.01f)
        {
            Color a = Color.Lerp(gainColor, originalColor, x);
            //Debug.Log(image.gameObject.name + "// " + " In gain fade #2");
            image.color = a;
            yield return new WaitForSeconds(0.01f);
        }

        yield return new WaitForSeconds(0.01f);
        image.color = originalColor;
    }
}
