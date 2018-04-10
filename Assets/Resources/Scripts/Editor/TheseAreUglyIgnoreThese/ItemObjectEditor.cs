#if DEBUG
using UnityEngine;
using System.Collections;
using UnityEditor;
using System.Collections.Generic;

[CustomEditor(typeof(ItemObject))]
public class ItemObjectEditor : Editor
{
    public override void OnInspectorGUI()
    {
        var itemObject = (target as ItemObject);

        EditorGUILayout.Space();

        if (ItemClassManager.itemClassNames.Count > 0)
        {

            itemObject.index = EditorGUILayout.Popup(itemObject.index, ItemClassManager.itemClassNames.ToArray());

            string itemScript = ItemClassManager.itemClassNames[itemObject.index];
            itemObject.itemToSpawn = itemScript;
        }
    }

    
}
#endif
