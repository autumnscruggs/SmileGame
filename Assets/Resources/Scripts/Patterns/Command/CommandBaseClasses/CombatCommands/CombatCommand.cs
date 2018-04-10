using UnityEngine;
using System.Collections;
using Commands;
using System;

public class CombatCommand : Command, ICombatCommand
{
    public CombatManager CombatManager {  get {  return combatManager;  } set {  combatManager = value; }  }
    private CombatManager combatManager;

    public CombatCommand()
    {
        combatManager = GameObject.FindObjectOfType<CombatManager>();
    }

    public void TakeCombatAction(GameObject gO)
    {
        Execute(gO);
        combatManager.PerformTurn();
    }
}
