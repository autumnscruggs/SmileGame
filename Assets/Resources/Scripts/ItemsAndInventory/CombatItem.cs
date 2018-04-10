using UnityEngine;
using System.Collections;

public abstract class CombatItem : Item
{
    public CombatParticipant User { get; protected set; }

    public override void PickUp(ICollector iCollector)
    {
        if (iCollector != null && iCollector is PlayerInventory)
        {
            PlayerInventory inventory = (PlayerInventory)iCollector;
            User = inventory.gameObject.GetComponent<Player>();
        }

        base.PickUp(iCollector);
    }

    protected bool UserExists()
    {
        return User != null;
    }
}
