using UnityEngine;
using System.Collections;
using System;
using System.Collections.Generic;
using Observer;

public class PlayerInventory : MonoBehaviour, ICollector, ISubject, IObserver
{
    public Transform itemHolder;
    private CombatManager combatManager;
    private List<IObserver> observers;
    protected List<Item> itemsInInventory;
    public List<Item> ItemsInInventory { get { return itemsInInventory; } }

    public PlayerInventory()
    {
        observers = new List<IObserver>();
        itemsInInventory = new List<Item>();
    }

    void Start()
    {
        combatManager = GameObject.FindObjectOfType<CombatManager>();
        if (combatManager != null)
        { combatManager.Attach(this); }
    }



    public void CheckForUnusableItems()
    {
        Item item = itemsInInventory.Find(x => x.State == ItemState.Depleted);
        itemsInInventory.Remove(item);
    }

    public void CollectItem(ICollectible collectible)
    {
        Item item = (Item)collectible;
        if(item != null)
        {
            item.PickUp(this);
            itemsInInventory.Add(item);
            Notify(item);
        }
    }

    public void PutItemObjectAway(GameObject itemObject)
    {
        Destroy(itemObject, 0.1f);
    }

    void OnTriggerEnter2D(Collider2D collider)
    {
        ItemObject itemObject = collider.GetComponent<ItemObject>();

        if (itemObject != null && !itemObject.pickedUp)
        {
            SoundEffectManager.Instance.PlayItemCollect();

            itemObject.pickedUp = true;

            itemObject.AddItemInstance(itemHolder.gameObject);
            PutItemObjectAway(itemObject.gameObject);

            string itemName = itemObject.itemToSpawn;
            Item item = itemHolder.gameObject.GetComponent(itemName) as Item;
            //Debug.Log(item.ItemName);
            CollectItem(item);
        }
    }


    #region Observer 
    //Subject - Notifies Item GUI
    public void Attach(IObserver o)
    {
        observers.Add(o);
    }

    public void Detach(IObserver o)
    {
        observers.Remove(o);
    }

    public void Notify(object s)
    {
        //Debug.Log("Notifying observers of.... " + s);

        if (observers.Count > 0)
        {
            foreach (IObserver o in observers)
            {
                o.ObserverUpdate(this, s);
            }
        }
    }

    //Observer - Watches CombatManager
    public void ObserverUpdate(object sender, object message)
    {
        if(sender is CombatManager)
        {
            if(message is BattleState && (BattleState)message == BattleState.TurnOver)
            {
                CheckForUnusableItems();
            }
        }
    }
    #endregion
}
