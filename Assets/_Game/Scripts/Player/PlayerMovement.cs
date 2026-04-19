using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public Rigidbody2D rb { get; private set; }
    private float speed = 15f;
    private float jumpForce = 15f;

    [Header("Ground checker")]
    [SerializeField] private Transform checkerOrigin;
    [SerializeField] private LayerMask groundMask;
    [SerializeField] private float checkerRadius = 0.5f;
    public bool isGrounded { get; private set; } = false;

    private int airJumps = 1;

    private float movementTimerMax = 0.3f;
    private float movementTimer = 0f;

    private FreeParallax parallax;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        parallax = GetComponentInChildren<FreeParallax>();
    }

    void FixedUpdate()
    {
        if (movementTimer < 0f)
        {
            float direction = Input.GetAxisRaw("Horizontal");
            Vector2 velocity = new Vector2(direction * speed, rb.linearVelocityY);
            rb.linearVelocity = velocity;
        }
    }


    private void Update()
    {
        movementTimer -= Time.deltaTime;
        CheckGround();

        if (Input.GetKeyDown(KeyCode.Space))
        {
            Jump();
        }

        UpdateParallax(rb.linearVelocityX);
    }

    private void Jump()
    {
        if (isGrounded)
        {
            rb.linearVelocityY = 0f;
            rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
        }
        
        if(!isGrounded && airJumps > 0)
        {
            airJumps --;
            rb.linearVelocityY = 0f;
            rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
        }

    }

    private void CheckGround()
    {
        if (Physics2D.OverlapCircle(checkerOrigin.position, checkerRadius, groundMask))
        {
            isGrounded = true;
            airJumps = 1;
        }
        else
        {
            isGrounded = false;
        }
    }

    public void ApplyCannonRecoil(Vector2 direction, float recoil)
    {
        movementTimer = movementTimerMax;
        rb.linearVelocity = Vector2.zero;
        rb.AddForce(direction * recoil, ForceMode2D.Impulse);
    }

    private void UpdateParallax(float speed)
    {
        parallax.Speed = (speed * -1f) * 0.25f;
    }
}
