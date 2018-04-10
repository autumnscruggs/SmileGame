using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class FlexibleGridSize : MonoBehaviour
{
    GridLayoutGroup grid;
    RectTransform rect;

    void Awake ()
    {
        grid = this.gameObject.GetComponent<GridLayoutGroup>();
        rect = this.gameObject.GetComponent<RectTransform>();
	}
	
    void Start()
    {
        grid.cellSize = new Vector2(rect.rect.width / 4.5f, rect.rect.height / 2);
    }
}
