using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class LevelManager 
{
    public static string CurrentLevel;
    public static int TotalLevels = 4;
    public static List<string> LevelsComplete = new List<string>();
    public static bool CanGoToBoss { get { return LevelsComplete.Count >= TotalLevels; } }

    public static void CompleteLevel()
    {
        LevelsComplete.Add(CurrentLevel);
    }
}
