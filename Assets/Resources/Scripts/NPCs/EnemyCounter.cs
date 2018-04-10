using UnityEngine;
using System.Collections;

public class EnemyCounter : MonoBehaviour
{
    private ChangeColorUponLevelSuccess[] colorables;
    [SerializeField] private Transform enemyHolder;
    public int maxEnemies { get; set; }
    public int enemiesCount { get; set; }

    public bool AllEnemiesDead { get { return enemiesCount <= 0; } }

    void Start()
    {
        colorables = GameObject.FindObjectsOfType<ChangeColorUponLevelSuccess>();
        maxEnemies = enemyHolder.GetComponentsInChildren<NPC>().Length;
    }

    void Update()
    {
        enemiesCount = System.Array.FindAll(enemyHolder.GetComponentsInChildren<NPC>(), item => item.combatState != CombatParticipant.CombatState.Dead).Length;
        if (AllEnemiesDead) { ColorEntireLevel(); }
    }

    private void ColorEntireLevel()
    {
        if (System.Array.Find(colorables, item => !item.Colored) != null)
        {
            foreach (ChangeColorUponLevelSuccess colorable in colorables)
            {
                if (!colorable.Colored)
                {
                    colorable.ChangeColor();
                }
            }
        }
    }
}
