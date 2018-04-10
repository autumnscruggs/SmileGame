using UnityEngine;
using System.Collections;

public class AllCombatParticipantCommands 
{
    public class SkipTurnCommand : CombatCommand
    {
        public SkipTurnCommand() : base()
        {
            this.commandName = "Skip Turn!";
        }

        public override void Execute(GameObject gObject)
        {
            var target = gObject.GetComponent<CombatParticipant>();
            base.Execute(gObject);
        }
    }
}
