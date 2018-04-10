#if DEBUG
using UnityEngine;
using UnityEditor; 
using System.Collections;

[CustomEditor(typeof(ItemManager))]
public class ItemSpriteManagerEditor : Editor
{
    private int leftSideWidth;
    private Vector2 scrollPos;

    void OnEnable()
    {
        leftSideWidth = 130;
    }

    public override void OnInspectorGUI()
    {
        EditorGUILayout.Space();
        ItemManager manager = (ItemManager)target;
        AddAndRemoveButtons(manager);

        scrollPos = EditorGUILayout.BeginScrollView(scrollPos, GUIStyle.none);
        EditorGUILayout.Space(); EditorGUILayout.Space();

        if (manager.itemSprites.Count > 0)
        {
            foreach (ItemManager.ItemDetails details in manager.itemSprites)
            {
                if (manager.itemSprites.IndexOf(details) != 0) { Border(); }
                DrawItemSprite(details, manager);
            }
        }

        EditorGUILayout.Space(); EditorGUILayout.Space(); EditorGUILayout.Space(); EditorGUILayout.Space();
        EditorGUILayout.EndScrollView();
    }

    private void Border()
    {
        EditorGUILayout.Space();
        EditorGUILayout.LabelField("__________________________________", CenterLabel());
        EditorGUILayout.Space();  EditorGUILayout.Space();
    }
    private void AddAndRemoveButtons(ItemManager manager)
    {
        EditorGUILayout.BeginHorizontal();
        if (GUILayout.Button("Add Details"))
        {
            manager.itemSprites.Add(new ItemManager.ItemDetails());
        }
        if (GUILayout.Button("Remove Details"))
        {
            manager.itemSprites.RemoveAt(manager.itemSprites.Count - 1);
        }
        EditorGUILayout.EndHorizontal();
    }
    private void DrawItemSprite(ItemManager.ItemDetails details, ItemManager manager)
    {
        EditorGUILayout.BeginHorizontal(); //full grid
        EditorGUILayout.BeginVertical(); //left side grid
        //Name picker
        ItemNamePicker(details);
        //Sprite picker
        details.Sprite = SpritePicker("Sprite: ", details.Sprite);
        //Description
        EditorGUILayout.BeginVertical();
        EditorGUILayout.LabelField("Description:", GUILayout.MaxWidth(leftSideWidth));
        EditorStyles.textField.wordWrap = true;
        details.ItemDescription = EditorGUILayout.TextArea(details.ItemDescription, GUILayout.MaxWidth(leftSideWidth), GUILayout.Height(50));
        EditorGUILayout.EndVertical();

        EditorGUILayout.EndVertical(); //end left side grid
        //Draw sprite
        if(details.Sprite !=null)
        DrawSpriteInInspector(80, details.Sprite);
        EditorGUILayout.Space(); //padding
        EditorGUILayout.EndHorizontal(); //end full grid
    }
    private void ItemNamePicker(ItemManager.ItemDetails details)
    {
        EditorGUILayout.BeginHorizontal(GUILayout.MaxWidth(leftSideWidth));
        EditorGUILayout.LabelField("Name: ", GUILayout.Width(43));
        if (ItemClassManager.itemClassNames.Count > 0)
        {
            details.index = EditorGUILayout.Popup(details.index, ItemClassManager.itemClassNames.ToArray());
            string itemScript = ItemClassManager.itemClassNames[details.index];
            details.ItemName = itemScript;
        }
        EditorGUILayout.EndHorizontal();
    }
    private Sprite SpritePicker(string label, Sprite referenceSprite)
    {
        EditorGUILayout.BeginHorizontal(GUILayout.MaxWidth(leftSideWidth));
        EditorGUILayout.LabelField(label, GUILayout.Width(43));
        Sprite sprite = referenceSprite;
        sprite = (Sprite)EditorGUILayout.ObjectField(sprite, typeof(Sprite), allowSceneObjects: true);
        EditorGUILayout.EndHorizontal();
        return sprite;
    }
    private void DrawSpriteInInspector(float width, Sprite spriteToDraw)
    {
        float aspect = (float)spriteToDraw.texture.width / (float)spriteToDraw.texture.height;
        Rect previewRect = GUILayoutUtility.GetAspectRect(aspect, GUILayout.Width(width), GUILayout.ExpandWidth(false));
        GUI.DrawTexture(previewRect, spriteToDraw.texture, ScaleMode.ScaleToFit, true, aspect);
    }
    private GUIStyle CenterLabel()
    {
        GUIStyle centerLabel = new GUIStyle(GUI.skin.label);
        centerLabel.alignment = TextAnchor.MiddleCenter;
        return centerLabel;
    }
}
#endif
