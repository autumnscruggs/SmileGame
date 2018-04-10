using UnityEngine;
using System.Collections;

public class PlayerCommands
{
    public class SmileCommand : CombatCommand
    {
        public SmileCommand() : base()
        {
            this.commandName = "Smiled!";
        }

        public override void Execute(GameObject gObject)
        {
            var target = gObject.GetComponent<Player>();
            if (target is Player)
            {
                target.Smile();
            }
            base.Execute(gObject);
        }
    }

    public class ComplimentCommand : CombatCommand
    {
        public ComplimentCommand() : base()
        {
            this.commandName = "Complimented!";
        }

        public override void Execute(GameObject gObject)
        {
            var target = gObject.GetComponent<Player>();
            if (target is Player)
            {
                target.Compliment();
            }
            base.Execute(gObject);
        }
    }

    public class UseItemCommand : CombatCommand
    {
        public CombatItem itemToUse;

        public UseItemCommand(CombatItem item) : base()
        {
            this.commandName = "Used Item!";
            itemToUse = item;
        }

        public override void Execute(GameObject gObject)
        {
            this.commandName = "Used " + itemToUse.ItemName + "!";
            itemToUse.Use();

            base.Execute(gObject);
        }
    }

}


