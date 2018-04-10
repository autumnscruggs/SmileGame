using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ItemManager : MonoBehaviour
{
    [System.Serializable]
    public class ItemDetails
    {
        public int index;
        public string ItemName;
        public string ItemDescription;
        public Sprite Sprite;
    }

    public List<ItemDetails> itemSprites;
   

    void Update()
    {
        //foreach (ItemDetails iCN in itemSprites)
        //{
        //    Debug.Log(iCN.ItemName);
        //    Debug.Log(iCN.ItemDescription);
        //}
    }
}
