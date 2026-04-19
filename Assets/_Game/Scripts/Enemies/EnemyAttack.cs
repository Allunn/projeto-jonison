using UnityEngine;

public class EnemyAttack : MonoBehaviour
{
    [SerializeField] private float detectionRange = 8f;
    [SerializeField] private LayerMask playerLayer;

    [SerializeField] GameObject bullet;

    private Transform playerPos;

    private EnemyHealth health;

    private float attackTime = 0f;
    private float attackTimeMax = 1.5f;

    private void Start()
    {
        health = GetComponent<EnemyHealth>();
    }

    void Update()
    {
        if (health.isDead) return;

        DetectPlayer();

        if (playerPos != null && attackTime <= 0f)
        {
            attackTime = attackTimeMax;

            Vector2 direction = (playerPos.position - transform.position).normalized;
            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
            var bulletGO = Instantiate(bullet, transform.position, Quaternion.Euler(new Vector3(0f, 0f, angle)), null);
            bulletGO.GetComponent<Rigidbody2D>().linearVelocity = direction * 7.5f;
        }

        attackTime -= Time.deltaTime;
    }

    private void DetectPlayer()
    {
        Collider2D hit = Physics2D.OverlapCircle(transform.position, detectionRange, playerLayer);

        if (hit != null)
        {
            playerPos = hit.transform;
        }
        else
        {
            playerPos = null;
        }
    }
}
