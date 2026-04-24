using UnityEngine;

public class EnemyBullet : MonoBehaviour
{
    private void OnCollisionEnter2D(Collision2D collision)
    {
        Destroy(gameObject);

        if (collision.gameObject.CompareTag("Player"))
        {
            if(collision.gameObject.TryGetComponent<PlayerHealth>(out PlayerHealth player))
            {
                player.TakeDamage(1);
            }
        }
    }
}
