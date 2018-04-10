using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using Observer;
using System.Collections.Generic;

public class PauseMenuInventory : MonoBehaviour, ISubject
{
    private ItemManager itemManager;
    private PlayerInventory inventory;

    public GameObject inventoryPanel;

    public GameObject itemDisplay;
    public GameObject noItemsText;

    private Item itemSelected;
    public Text itemName;
    public Text itemDescription;
    public Image itemSprite;
    public GameObject useItemButton;

    private List<IObserver> observers;
    public const string UpdateTheGUI = "Update Pls";

    void Awake()
    {
        observers = new List<IObserver>();
        itemManager = GameObject.FindObjectOfType<ItemManager>();
        inventory = GameObject.FindObjectOfType<PlayerInventory>();
        ToggleInventory(false);
    }


    void Update()
    {

    }

    public void ItemButton(Item item)
    {
        itemDisplay.SetActive(true);
        itemSelected = item;
        UpdateGUI();
    }

    private void UpdateGUI()
    {
        itemName.text = itemSelected.ItemName + " x" + (itemSelected.numberAllowed);
        itemDescription.text = itemManager.itemSprites.Find(x => x.ItemName == itemSelected.GetType().ToString()).ItemDescription;
        itemSprite.sprite = itemManager.itemSprites.Find(x => x.ItemName == itemSelected.GetType().ToString()).Sprite;
        useItemButton.SetActive(itemSelected.CanBeUsedInMainInventory);
    }

    public void UseItemButton()
    {
        if (itemSelected.CanBeUsedInMainInventory && itemSelected.State != ItemState.Depleted)
        {
            itemSelected.Use();
            Notify(UpdateTheGUI);
            UpdateGUI();
        }

        if(itemSelected.numberAllowed <= 0)
        {
            itemDisplay.SetActive(false);
            CheckForNoItemDisplay();
        }
    }

    private void CheckForNoItemDisplay()
    {
        if (inventory.ItemsInInventory.Count < 1)
        {
            noItemsText.SetActive(true);
        }
        else
        {
            noItemsText.SetActive(false);
        }
    }

    public void ToggleInventory()
    {
        CheckForNoItemDisplay();
        inventoryPanel.SetActive(!inventoryPanel.activeInHierarchy);
        itemDisplay.SetActive(false);
    }
    public void ToggleInventory(bool toggle)
    {
        if (inventory != null) { CheckForNoItemDisplay(); }
        inventoryPanel.SetActive(toggle);
        itemDisplay.SetActive(false);
    }

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
        foreach(IObserver o in observers)
        {
            o.ObserverUpdate(this, s);
        }
    }
}