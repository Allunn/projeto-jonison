using UnityEngine;

public class EnemyMovement : MonoBehaviour
{

    [SerializeField] private bool enemyMovement = true; 

    public Rigidbody2D rb { get; private set; }

    private float dir = 1f;

    [SerializeField] private Transform checkOrigin;
    [SerializeField] private LayerMask groundLayer;
    [SerializeField] private float checkDistance = 1f;

    private bool lookingWall = false;
    private bool lookingHole = false;

    [SerializeField] private float speed = 6f; 

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void FixedUpdate()
    {
        if (enemyMovement)
        {
            rb.linearVelocityX = dir * speed;
        }
    }

    private void Update()
    {
        if (enemyMovement)
        {
            DetectHole();
            DetectWall();

            if (lookingWall || lookingHole) 
            {
                ChangeDirection();
            }
        }
    }

    private void ChangeDirection()
    {
        if (dir > 0f)
        {
            dir = -1f;
        }
        else
        {
            dir = 1f;
        }
        transform.Rotate(0f, 180f, 0f);
    }

    private void DetectHole()
    {
        if (Physics2D.Raycast(checkOrigin.position, -transform.up, checkDistance, groundLayer))
        {
            lookingHole = false;
        }
        else
        {
            lookingHole = true;
        }
    }

    private void DetectWall()
    {
        if (Physics2D.Raycast(checkOrigin.position, transform.right, checkDistance, groundLayer))
        {
            lookingWall = true;
        }
        else
        {
            lookingWall = false;
        }
    }
}
