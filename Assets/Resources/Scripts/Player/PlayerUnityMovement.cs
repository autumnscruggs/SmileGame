using UnityEngine;
using System.Collections;

[RequireComponent(typeof(PlayerController))]
public class PlayerUnityMovement : MonoBehaviour
{
    private PlayerController controller;
    public float movementSpeed = 10f;
    private Vector3 moveTranslation;
    public bool movement2D = false;
    private bool canMove = true;
    public bool usingRigidbody;

    void Awake()
    {
        controller = this.GetComponent<PlayerController>();
    }

	void Update ()
    {
        if (controller.MovementInput && canMove)
        { UpdateMovement(); }
    }

    private void UpdateMovement()
    {
        CreateMoveTranslationBasedOnPerspective();
        if (!usingRigidbody)
        { this.transform.position += moveTranslation; }
        else
        {
            if (movement2D)
            {
                this.GetComponent<Rigidbody2D>().AddForce(moveTranslation);
            }
            else
            {
                this.GetComponent<Rigidbody>().AddForce(moveTranslation);
            }
        }
    }

    private void CreateMoveTranslationBasedOnPerspective()
    {
        if (movement2D)
        { moveTranslation = new Vector3(controller.direction.x, controller.direction.y, 0) * Time.deltaTime * movementSpeed; }
        else
        { moveTranslation = new Vector3(controller.direction.x, 0, controller.direction.y) * Time.deltaTime * movementSpeed; }
    }

    public void ToggleMovement(bool enabled)
    {
        canMove = enabled;
    }
}
