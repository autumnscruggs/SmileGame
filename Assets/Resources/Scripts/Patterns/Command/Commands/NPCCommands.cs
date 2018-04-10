using UnityEngine;
using System.Collections;

public class NPCCommands
{
    public class AttackCommand : CombatCommand
    {
        public AttackCommand() : base()
        {
            this.commandName = "Attack!";
        }

        public override void Execute(GameObject gObject)
        {
            var target = gObject.GetComponent<NPC>();
            if (target is NPC)
            {
                target.DamageMove();
            }
            base.Execute(gObject);
        }
    }

    public class HealCommand : CombatCommand
    {
        public HealCommand() : base()
        {
            this.commandName = "Heal!";
        }

        public override void Execute(GameObject gObject)
        {
            var target = gObject.GetComponent<NPC>();
            if (target is NPC)
            {
                target.HealMove();
            }
            base.Execute(gObject);
        }
    }
}

public class MiniBossCommands
{
    public class MiniBossSpecial : CombatCommand
    {
        public MiniBossSpecial() : base()
        {
            this.commandName = "Mini Boss Attack!";
        }

        public override void Execute(GameObject gObject)
        {
            var target = gObject.GetComponent<MiniBoss>();
            if (target is MiniBoss)
            {
                target.SpecialMove();
            }
            base.Execute(gObject);
        }
    }
}

public class FinalBossCommands
{
    public class FinalBossFirstSpecial : CombatCommand
    {
        public FinalBossFirstSpecial() : base()
        {
            this.commandName = "Final Boss Special One!";
        }

        public override void Execute(GameObject gObject)
        {
            var target = gObject.GetComponent<FinalBoss>();
            if (target is FinalBoss)
            {
                target.FirstSpecialMove();
            }
            base.Execute(gObject);
        }
    }

    public class FinalBossSecondSpecial : CombatCommand
    {
        public FinalBossSecondSpecial() : base()
        {
            this.commandName = "Final Boss Special Two!";
        }

        public override void Execute(GameObject gObject)
        {
            var target = gObject.GetComponent<FinalBoss>();
            if (target is FinalBoss)
            {
                target.SecondSpecialMove();
            }
            base.Execute(gObject);
        }
    }
     
}
