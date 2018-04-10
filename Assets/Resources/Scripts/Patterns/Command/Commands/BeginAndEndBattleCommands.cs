using UnityEngine;
using System.Collections;
using Commands;

public class BeginAndEndBattleCommands
{
}

public class BeginBattleCommand : Command
{
    public BeginBattleCommand() : base()
    {
        this.commandName = "Battle Begins";
    }

    public override void Execute(GameObject gObject)
    {
        var target = gObject.GetComponent<CombatParticipant>();
        var manager = GameObject.FindObjectOfType<CombatManager>();
        if (target is CombatParticipant)
        {
            manager.Attach(target);
            manager.Attach(target.Target);
            manager.BeginBattle();
        }
        base.Execute(gObject);
    }
}

public class EndBattleCommand : Command
{
    public EndBattleCommand() : base()
    {
        this.commandName = "Battle Is Over";
    }

    public override void Execute(GameObject gObject)
    {
        var cMTarget = gObject.GetComponent<CombatManager>();
        if (cMTarget is CombatManager)
        {
            cMTarget.EndBattle();
        }

        var cPTarget = gObject.GetComponent<CombatParticipant>();
        if(cPTarget is CombatParticipant)
        {
            cPTarget.LoseBattle();
        }
        base.Execute(gObject);
    }
}


