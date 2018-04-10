using UnityEngine;
using System.Collections;
using System;

public class ItemObject : MonoBehaviour
{
    [HideInInspector] public int index = 0;
    [HideInInspector] public bool pickedUp = false;
    public string itemToSpawn;

    public void AddItemInstance(GameObject itemObject)
    {
        if(itemObject.GetComponent(itemToSpawn) == null)
        {
            //UnityEngineInternal.APIUpdaterRuntimeServices.AddComponent(itemObject, "Assets/Resources/Scripts/ItemsAndInventory/ItemObject.cs (9,9)", itemToSpawn);
            itemObject.gameObject.AddComponent(Type.GetType(itemToSpawn));
        }
    }
}
