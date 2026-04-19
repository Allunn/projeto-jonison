using UnityEngine;

public class Bullet : MonoBehaviour
{
    private void OnCollisionEnter2D(Collision2D collision)
    {
        Destroy(gameObject);

        if (collision.gameObject.CompareTag("Enemy"))
        {
            if(collision.gameObject.TryGetComponent<EnemyHealth>(out EnemyHealth enemy))
            {
                enemy.TakeDamage();
            }
        }
    }
}
