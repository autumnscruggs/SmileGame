using UnityEngine;
using System.Collections;

public enum ItemState { Usable, Depleted }

public abstract class Item : MonoBehaviour, ICollectible
{
    public string ItemName { get; protected set; }
    public bool CanBeUsedInMainInventory { get; protected set; }

    //multiple numbers of the item
    public int numberAllowed;

    public ItemState State { get; set; }

    public virtual void PickUp(ICollector iCollector)
    {
        if(numberAllowed <= 0)
        { this.State = ItemState.Usable;  }

        numberAllowed++;
    }

    public virtual void Use()
    {
        numberAllowed--;

        if (numberAllowed <= 0)
        {
            State = ItemState.Depleted;
        }

    }
}
