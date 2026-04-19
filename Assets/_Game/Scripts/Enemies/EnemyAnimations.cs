using UnityEngine;

public class EnemyAnimations : MonoBehaviour
{
    private Animator animator;

    public AnimationClip idle;
    public AnimationClip walk;
    public AnimationClip death;
    public AnimationClip attack;

    private EnemyMovement movement;
    private EnemyHealth health;

    void Start()
    {
        animator = GetComponent<Animator>();
        movement = GetComponent<EnemyMovement>();
        health = GetComponent<EnemyHealth>();
    }

    void Update()
    {
        if (health.isDead)
        {
            if (death != null) animator.Play(death.name);
        }
        else
        {
            if (movement.rb.linearVelocityX != 0f)
            {
                if (walk != null) animator.Play(walk.name);
            }
            else
            {
                if (idle != null) animator.Play(idle.name);
            }
        }
    }
}
