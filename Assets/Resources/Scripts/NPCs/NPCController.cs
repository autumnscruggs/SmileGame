using UnityEngine;
using System.Collections;

public enum NPCType { Fearful, Aggressive, Neutral, Mini_Boss }

public class NPCController : MonoBehaviour
{
    public float speed = 3f;

    private float wanderingTime;
    private float stopTime;
    private float stopTimer;
    private float wanderingTimer;
    public NPCType type;

    private Player player;
    private Vector2 direction;

    private bool inRangeOfPlayer;

    public bool wanderingFromHome = true;
    public float travelDistance = 7f;
    private Vector2 originalPosition;
    public float distanceFromHome;

    public bool canMove = true;

    void Awake ()
    {
        player = GameObject.FindObjectOfType<Player>();
        originalPosition = this.transform.position;

        inRangeOfPlayer = false;
        ChangeDirection();
        wanderingTimer = 0;
        stopTimer = 0;
    }

    private void OnDrawGizmos()
    {
        //Gizmos.color = Color.yellow;
        //Gizmos.DrawSphere(originalPosition, 0.5f);
    }

    private void ChangeDirection()
    {
        if(distanceFromHome > travelDistance)
        {
            wanderingFromHome = false;
        }
        else
        {
            wanderingFromHome = true;

            do { wanderingTime = Random.Range(3, 11); }
            while (wanderingTime == 0);

            do { stopTime = Random.Range(1, 4); }
            while (stopTime == 0);

            do { direction = new Vector2(Random.Range(-1, 2), Random.Range(-1, 2)); }
            while (direction.x == 0 && direction.y == 0);
        }
    }

    void Update ()
    {
        if (canMove)
        {
            distanceFromHome = Vector3.Distance(this.transform.position, originalPosition);

            if (type != NPCType.Mini_Boss)
            {
                if (!inRangeOfPlayer) { NormalWandering(); }
            }
        }
    }

    public void SeesPlayer()
    {
        switch (type)
        {
            case NPCType.Neutral:
                NormalWandering();
                break;
            case NPCType.Aggressive:
                ChasePlayer();
                break;
            case NPCType.Fearful:
                RunFromPlayer();
                break;
        }
    }

    private void NormalWandering()
    {
        if (canMove)
        {
            WanderingAndStoppingTime();

            if (wanderingTimer < wanderingTime)
            { this.transform.position += (Vector3)direction.normalized * speed * Time.deltaTime; }
        }
    }

    private void WanderingAndStoppingTime()
    {
        if (canMove)
        {
            if (wanderingFromHome)
            {
                wanderingTimer += Time.deltaTime;
                if (wanderingTimer > wanderingTime)
                {
                    direction = Vector2.zero; //stop NPC

                    stopTimer += Time.deltaTime;
                    if (stopTimer > stopTime)
                    {
                        wanderingTimer = 0;
                        stopTimer = 0;
                        ChangeDirection();
                    }
                }
            }
            else
            {
                Vector2 heading = originalPosition - (Vector2)this.transform.position;
                var distance = heading.magnitude;
                direction = heading / distance;

                if (distanceFromHome <= 1f)
                {
                    ChangeDirection();
                }
            }
        }
    }

    private void ChasePlayer()
    {
        if (canMove) { this.transform.position = Vector2.MoveTowards(transform.position, player.transform.position, speed * Time.deltaTime); }
        
    }

    private void RunFromPlayer()
    {
        if (canMove)
        {
            direction = this.transform.position - player.transform.position;
            this.transform.Translate(direction.normalized * speed * Time.deltaTime);
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.GetComponent<NPCController>() != null)
        {
            Collider2D[] colliders = this.GetComponents<Collider2D>();
            Collider2D col = System.Array.Find(colliders, item => !item.isTrigger);
            Physics2D.IgnoreCollision(collision.collider, col);
        }
    }

    void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.GetComponent<NPCController>() == null)
        {
            ChangeDirection();
        }
    }

    void OnTriggerStay2D(Collider2D collider)
    {
        if (collider.GetComponent<Player>() != null)
        {
            inRangeOfPlayer = true;
            SeesPlayer();
        }
    }

    void OnTriggerExit2D()
    {
        inRangeOfPlayer = false;
    }
}
