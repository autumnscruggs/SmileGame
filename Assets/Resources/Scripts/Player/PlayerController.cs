using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PlayerController : MonoBehaviour
{
    //Keyboard Input
    [Header("Assign Keyboard Keys")]
    public KeyCode up = KeyCode.W;
    public KeyCode left = KeyCode.A;
    public KeyCode down = KeyCode.S;
    public KeyCode right = KeyCode.D;
    //direction vectors for movement
    [HideInInspector]
    public Vector2 direction = new Vector2(0, 0);
    [HideInInspector]
    public Vector2 keyDirection = new Vector2(0, 0);
    //Tests for input
    public bool MovementInput
    {
        get
        {
            //Debug.Log(direction.sqrMagnitude);
            if (direction.magnitude == 0) return false;
            return true;
        }
    }


    void Update()
    {
        GetInputsAndDirection();
    }

    private void GetInputsAndDirection()
    {
        //Resets the directions each frame
        ResetDirection();
        //Sets direction based on input
        RightAndLeft();
        UpAndDown();
        //sets the direction based on the keyDirection and normalizes it
        direction += keyDirection;
    }

    private void ResetDirection()
    {
        //Sets them both back to 0
        keyDirection = new Vector2(0, 0);
        direction = new Vector2(0, 0);
    }
    public virtual void RightAndLeft()
    {
        if (KeyboardInput.IsHoldingKey(right))
        {
            keyDirection.x += 1;
        }
        if (KeyboardInput.IsHoldingKey(left))
        {
            keyDirection.x += -1;
        }
    }
    public virtual void UpAndDown()
    {
        if (KeyboardInput.IsHoldingKey(up))
        {
            keyDirection.y += 1;
        }
        if (KeyboardInput.IsHoldingKey(down))
        {
            keyDirection.y = -1;
        }
    }

}
