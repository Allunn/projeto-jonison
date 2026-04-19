using UnityEngine;

public class PlayerAnimations : MonoBehaviour
{
    private PlayerMovement movement;
    private PlayerHealth health;
    private Animator animator;

    [Header("Animações")]
    public AnimationClip idle;
    public AnimationClip walk;
    public AnimationClip hit;
    public AnimationClip airUp;
    public AnimationClip airDown;
    public AnimationClip death;

    void Start()
    {
        movement = GetComponent<PlayerMovement>();
        health = GetComponent<PlayerHealth>();
        animator = GetComponent<Animator>();
    }


    void Update()
    {
        if (health.isDead)
        {
            animator.Play(death.name);
        }
        else
        {
            if (movement.isGrounded)
            {
                if (movement.rb.linearVelocityX != 0f)
                {
                    animator.Play(walk.name);
                }
                else
                {
                    animator.Play(idle.name);
                }
            }
            else
            {
                if (movement.rb.linearVelocityY > 0f)
                {
                    animator.Play(airUp.name);
                }
                else
                {
                    animator.Play(airDown.name);
                }
            }
        }
    }
}
