using UnityEngine;
using System.Collections;
using Observer;
using System;
using System.Collections.Generic;

public class ItemGUI : MonoBehaviour, IObserver
{
    public GameObject combatItemContainer;
    public Transform combatItemGrid;
    public GameObject combatItemButtonPrefab;

    public Transform mainInventoryItemGrid;
    public GameObject mainInventoryItemButtonPrefab;

    private CombatManager combatMan;
    private PlayerInventory inventory;
    private PauseMenuInventory pauseMenuInventory;

    public List<Item> itemsWithGUI;
    public List<ItemGUIButton> buttons;

    void Awake()
    {
        itemsWithGUI = new List<Item>();
        buttons = new List<ItemGUIButton>();
        inventory = GameObject.FindObjectOfType<PlayerInventory>();
        combatMan = GameObject.FindObjectOfType<CombatManager>();
        pauseMenuInventory = GameObject.FindObjectOfType<PauseMenuInventory>();
    }

    void Start()
    {
        inventory.Attach(this);
        combatMan.Attach(this);
        pauseMenuInventory.Attach(this);
        ToggleCombatItemGUI(false);
    }

    public void ToggleCombatItemGUI(bool show)
    {
        combatItemContainer.SetActive(show);
    }

    private void ListExistingItems()
    {
        foreach(CombatItem i in inventory.ItemsInInventory)
        {
            CreateNewButton(i);
        }
    }

    private void UpdateButtonGUI()
    {
        List<ItemGUIButton> iGBsToRemove = buttons.FindAll(x => x.itemToUse.State == ItemState.Depleted);

        foreach(ItemGUIButton igb in iGBsToRemove)
        {
            RemoveButton(igb);
        }
    }

    private void CreateNewButton(Item item)
    {
        //Instantiate In Combat Grid
        GameObject newItemButton = Instantiate(combatItemButtonPrefab, combatItemGrid) as GameObject;
        ItemGUIButton iGB = newItemButton.GetComponent<ItemGUIButton>();
        iGB.GetComponentInChildren<UnityEngine.UI.Text>().text = item.ItemName + " x1";
        iGB.itemToUse = item;
        itemsWithGUI.Add(item);
        buttons.Add(iGB);

        //Instantiate In Main Inventory Grid
        GameObject newInventoryItem = Instantiate(mainInventoryItemButtonPrefab, mainInventoryItemGrid) as GameObject;
        ItemGUIButton iGB2 = newInventoryItem.GetComponent<ItemGUIButton>();
        iGB2.GetComponentInChildren<UnityEngine.UI.Text>().text = item.ItemName + " x1";
        iGB2.itemToUse = item;
        buttons.Add(iGB2);
    }

    private void RemoveButton(ItemGUIButton iGB)
    {
        //Debug.Log("Removing..." + iGB.name);
        buttons.Remove(iGB);
        itemsWithGUI.Remove(iGB.itemToUse);
        Destroy(iGB.gameObject);
    }


    public void ObserverUpdate(object sender, object message)
    {
        if(sender is PauseMenuInventory)
        {
            if(message.ToString() == PauseMenuInventory.UpdateTheGUI)
            {
                //Debug.Log("Updating GUI");
                UpdateButtonGUI();
            }
        }

        if (sender is PlayerInventory)
        {
            if (message is Item)
            {
                Item item = (Item)message;
                if (!itemsWithGUI.Contains(item))
                { CreateNewButton(item); }
            }
        }

        if(sender is CombatManager)
        {
            if(message is BattleState)
            {
                switch ((BattleState)message)
                {
                    case BattleState.TurnOver:
                        ToggleCombatItemGUI(false);
                        UpdateButtonGUI();
                        break;
                    case BattleState.BattleBegins:
                        UpdateButtonGUI();
                        break;
                }
            }
        }
    }
}
