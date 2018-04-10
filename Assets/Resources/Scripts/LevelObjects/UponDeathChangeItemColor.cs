using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class UponDeathChangeItemColor : MonoBehaviour
{
    private NPC npc;
    private Vector3 originalPosition;

    public List<ChangeColorUponLevelSuccess> objectsWithinRaidus;
    private bool changedColors = false;

    public float colorRadius = 10f;

    public bool foundObjects { get; private set; }

    void Awake()
    {
        objectsWithinRaidus = new List<ChangeColorUponLevelSuccess>();
        npc = this.GetComponent<NPC>();
        originalPosition = npc.transform.position;
        GetColorableObjectsWithinRadius();
    }

    void Start()
    {

    }

    void Update()
    {
        //if (KeyboardInput.IsKeyDown(KeyCode.N))
        //{
        //    ChangeColorOfObjectsInRadius();
        //}

        if (npc.combatState == CombatParticipant.CombatState.Dead && !changedColors)
        {
            ChangeColorOfObjectsInRadius();
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(originalPosition, colorRadius);
    }

    private void GetColorableObjectsWithinRadius()
    {
        Collider2D[] hitColliders = Physics2D.OverlapCircleAll(originalPosition, colorRadius);
        foreach (Collider2D collider in hitColliders)
        {
            //Debug.Log(collider.name);

            if (collider.gameObject.GetComponent<ChangeColorUponLevelSuccess>() != null)
            {
                ChangeColorUponLevelSuccess changeColor = collider.gameObject.GetComponent<ChangeColorUponLevelSuccess>();
                if (!objectsWithinRaidus.Contains(changeColor))
                {
                    objectsWithinRaidus.Add(changeColor);
                }
            }
        }

        foundObjects = true;
    }

    private void ChangeColorOfObjectsInRadius()
    {
       foreach(ChangeColorUponLevelSuccess colorable in objectsWithinRaidus)
        {
            //Debug.Log(colorable.name);
            colorable.ChangeColor();
        }

        changedColors = true;
    }
}
