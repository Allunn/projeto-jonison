using UnityEngine;
using UnityEngine.SceneManagement;

public class EnemyHealth : MonoBehaviour
{
    private Collider2D col;
    public bool isDead { get; private set; } = false;
    public AnimationClip deathAnim;

    private void Start()
    {
        col = GetComponent<Collider2D>();
    }

    public void TakeDamage()
    {
        Die();
    }

    private void Die()
    {
        isDead = true;
        col.enabled = false;

        Invoke(nameof(DestroyEnemy), deathAnim.length);
    }

    private void DestroyEnemy()
    {
        Destroy(gameObject);
    }
}
