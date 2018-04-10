using UnityEngine;
using System.Collections;

public class MainInventoryItemGUIButton : ItemGUIButton {

    private PauseMenuInventory inventory;

    void Awake()
    {
        inventory = GameObject.FindObjectOfType<PauseMenuInventory>();
    }

    public override void OnClick()
    {
        inventory.ItemButton(this.itemToUse);
    }
}
