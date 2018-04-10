using UnityEngine;
using System.Collections;

namespace Commands
{
    public interface ICombatCommand : ICommand
    {
        CombatManager CombatManager { get; set; }

        void TakeCombatAction(GameObject gO);
    }
}

