using UnityEngine;
using System.Collections;

public class TutorialNPC : NormalCrierNPC
{
    private int turnsPassed = 0;

    protected override void FullAIDecisionTree() //"AI"
    {
        switch (turnsPassed)
        {
            case 0:
                CommandCreator.God.ExecuteNPCAttack(this.gameObject, damageMoveName);
                break;
            case 1:
                CommandCreator.God.ExecuteNPCHeal(this.gameObject, healMoveName);
                break;
            default:
                CommandCreator.God.ExecuteNPCAttack(this.gameObject, damageMoveName);
                break;
        }

        turnsPassed++;
    }
}
