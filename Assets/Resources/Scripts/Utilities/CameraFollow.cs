using UnityEngine;
using System.Collections;

public class CameraFollow : MonoBehaviour
{
    private enum FollowType
    {
        Locked,
        Lerp
    }

    public Transform characterToFollow;
    [SerializeField] private float cameraYOffset = 3f;
    [SerializeField] private float cameraXOffset = 4f;
    [SerializeField] private float lerpSpeed = 5f;
    [SerializeField] FollowType followType;


    void Update()
    {
        switch (followType)
        {
            case FollowType.Locked:
                DoCameraLockBehavior();
                break;
            case FollowType.Lerp:
                DoCameraLerpBehavior();
                break;
            default:
                break;
        }
    }

    private void DoCameraLockBehavior()
    {
        this.transform.position = new Vector3(characterToFollow.position.x + cameraXOffset,
            characterToFollow.position.y + cameraYOffset, this.transform.position.z);

    }

    private void DoCameraLerpBehavior()
    {
        Vector3 targetPosition = Vector3.Lerp(this.transform.position,
            characterToFollow.position, lerpSpeed * Time.deltaTime);

        targetPosition.z = this.transform.position.z;
        targetPosition.y = characterToFollow.position.y + cameraYOffset;
        transform.position = targetPosition;

    }
}
