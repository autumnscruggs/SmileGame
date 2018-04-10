using UnityEngine;
using System.Collections;

[RequireComponent(typeof(SpriteRenderer))]
public class ChangeColorUponLevelSuccess : MonoBehaviour
{
    //private LevelEndDoor endDoor;
    private SpriteRenderer sRenderer;
    private Sprite originalSprite;
    public Sprite newSprite;
    public bool Colored { get; private set; }

    void Awake()
    {
        //endDoor = GameObject.FindObjectOfType<LevelEndDoor>();
        sRenderer = this.GetComponent<SpriteRenderer>();
        originalSprite = sRenderer.sprite;
        Colored = false;
    }

    public void ChangeColor()
    {
        sRenderer.sprite = newSprite;
        Colored = true;
    }
}
