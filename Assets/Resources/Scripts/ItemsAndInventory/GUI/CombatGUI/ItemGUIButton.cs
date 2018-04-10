using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class ItemGUIButton : MonoBehaviour
{
    public Item itemToUse;
    private Player player;

    public virtual void OnClick()
    {
        CommandCreator.God.ExecuteUseItem(itemToUse.gameObject, player.gameObject, (CombatItem)itemToUse);
    }

    private void OnEnable()
    {
        this.GetComponent<RectTransform>().localScale = Vector3.one;
    }

    private void Awake()
    {
        player = GameObject.FindObjectOfType<Player>();
    }

    void Update()
    {
        this.GetComponentInChildren<Text>().text = itemToUse.ItemName + " x" + itemToUse.numberAllowed;
    }
}
