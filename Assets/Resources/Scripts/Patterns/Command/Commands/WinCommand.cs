using UnityEngine;
using System.Collections;
using Commands;

public class WinCommand : Command
{
    public WinCommand() : base()
    {
        this.commandName = "Won!";
    }

    public override void Execute(GameObject gObject)
    {
        var target = gObject.GetComponent<CombatManager>();
        if (target is CombatManager)
        {
            string winner;
            if(target.turn == Turn.NPC) { winner = target.participants.Find(item => item is Player).CommanderName; }
            else { winner = target.participants.Find(item => item is NPC).CommanderName; }
            this.commandName = winner + " Won!";
            target.StartEndBattleTimer();
        }
        base.Execute(gObject);
    }
}
