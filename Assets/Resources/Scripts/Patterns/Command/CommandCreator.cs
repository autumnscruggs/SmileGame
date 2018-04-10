using UnityEngine;
using System.Collections;
using Commands;

public class CommandCreator : MonoBehaviour
{
    private static CommandCreator singleton; // Singleton instance   
    public static CommandCreator God
    {
        get
        {
            if (singleton == null)
            {
                Debug.LogError("[CommandCreator]: Instance does not exist!");
                return null;
            }

            return singleton;
        }
    }

    void Awake()
    {
        #region Singleton
        // Found a duplicate instance of this class, destroy it!
        if (singleton != null && singleton != this)
        {
            Destroy(this.gameObject);
            return;
        }
        else
        {
            // Create singleton instance
            singleton = this;
            DontDestroyOnLoad(this.gameObject);
        }
        #endregion
    }

    private CommandProcessor commandProcessor;

    void Start()
    {
        commandProcessor = GameObject.FindObjectOfType<CommandProcessor>();
    }

    //---------------Begin And End Commands----------------//
    public void ExecuteBeginCombat(GameObject objectToActOn)
    {
        BeginBattleCommand command = new BeginBattleCommand();
        commandProcessor.ExecuteCommand(command, objectToActOn);
    }

    public void ExecuteEndCombat(GameObject objectToActOn)
    {
        EndBattleCommand command = new EndBattleCommand();
        commandProcessor.ExecuteCommand(command, objectToActOn);
    }

    //----------------Victory Commands---------------------//
    public void ExecuteWin(GameObject objectToActOn)
    {
        WinCommand command = new WinCommand();
        commandProcessor.ExecuteCommand(command, objectToActOn);
    }

    //----------------Player & NPC Commands---------------//

    public void ExecuteSkipTurn(GameObject participant, string name)
    {
        AllCombatParticipantCommands.SkipTurnCommand skipCommand = new AllCombatParticipantCommands.SkipTurnCommand();
        skipCommand.commandName = participant.GetComponent<CombatParticipant>().CommanderName + " " + name;
        commandProcessor.ExecuteCommand(skipCommand, participant);
    }

    //----------------Player Commands---------------------//

    public void ExecuteSmile(GameObject player)
    {
        PlayerCommands.SmileCommand smileCommand = new PlayerCommands.SmileCommand();
        string name = player.GetComponent<CombatParticipant>().CommanderName + " " + smileCommand.commandName;
        smileCommand.commandName = name;
        commandProcessor.ExecuteCommand(smileCommand, player);
    }

    public void ExecuteCompliment(GameObject player)
    {
        PlayerCommands.ComplimentCommand complimentCommand = new PlayerCommands.ComplimentCommand();
        string name = player.GetComponent<CombatParticipant>().CommanderName + " " + complimentCommand.commandName;
        complimentCommand.commandName = name;
        commandProcessor.ExecuteCommand(complimentCommand, player);
    }

    public void ExecuteUseItem(GameObject itemObj, GameObject player, CombatItem item)
    {
        PlayerCommands.UseItemCommand itemCommand = new PlayerCommands.UseItemCommand(item);
        string name = player.GetComponent<CombatParticipant>().CommanderName + " " + itemCommand.commandName;
        itemCommand.commandName = name;
        commandProcessor.ExecuteCommand(itemCommand, itemObj);
    }

    //----------------NPC Commands---------------------//

    public void ExecuteNPCAttack(GameObject npc, string name)
    {
        NPCCommands.AttackCommand attackCommand = new NPCCommands.AttackCommand();
        attackCommand.commandName = npc.GetComponent<CombatParticipant>().CommanderName + " " + name;
        commandProcessor.ExecuteCommand(attackCommand, npc);
    }

    public void ExecuteNPCHeal(GameObject npc, string name)
    {
        NPCCommands.HealCommand healCommand = new NPCCommands.HealCommand();
        healCommand.commandName = npc.GetComponent<CombatParticipant>().CommanderName + " " + name;
        commandProcessor.ExecuteCommand(healCommand, npc);
    }

    public void ExecuteMiniBossSpecial(GameObject npc, string name)
    {
        MiniBossCommands.MiniBossSpecial specialCommand = new MiniBossCommands.MiniBossSpecial();
        specialCommand.commandName = npc.GetComponent<CombatParticipant>().CommanderName + " " + name;
        commandProcessor.ExecuteCommand(specialCommand, npc);
    }

    public void ExecuteFinalBossFirstSpecial(GameObject npc, string name)
    {
        FinalBossCommands.FinalBossFirstSpecial specialCommand = new FinalBossCommands.FinalBossFirstSpecial();
        specialCommand.commandName = npc.GetComponent<CombatParticipant>().CommanderName + " " + name;
        commandProcessor.ExecuteCommand(specialCommand, npc);
    }

    public void ExecuteFinalBossSecondSpecial(GameObject npc, string name)
    {
        FinalBossCommands.FinalBossSecondSpecial specialCommand = new FinalBossCommands.FinalBossSecondSpecial();
        specialCommand.commandName = npc.GetComponent<CombatParticipant>().CommanderName + " " + name;
        commandProcessor.ExecuteCommand(specialCommand, npc);
    }
}
