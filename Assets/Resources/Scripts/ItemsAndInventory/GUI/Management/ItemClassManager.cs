#if DEBUG
using UnityEngine;
using UnityEditor;
using System.Collections;
using System.Collections.Generic;

[InitializeOnLoad]
public class ItemClassManager : Editor
{
    public static List<MonoScript> itemScripts;
    public static List<string> itemClassNames;

    static ItemClassManager()
    {
        itemScripts = new List<MonoScript>();
        itemClassNames = new List<string>();
        FindItemClasses();
    }

    private static void FindItemClasses()
    {
        foreach (MonoScript script in Resources.LoadAll("Scripts/ItemsAndInventory/ConcreteItems"))
        {
            //Debug.Log(script.name);
            itemScripts.Add(script);
            itemClassNames.Add(script.name);
        }
    }

}
#endif
