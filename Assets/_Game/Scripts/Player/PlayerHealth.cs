using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour
{
    [SerializeField] private int maxHealth = 5;
    private int currentHealth;
    public bool isDead {  get; private set; } = false;

    [SerializeField] private Slider healthBar;

    private PlayerMovement move;
    private SpriteRenderer playerSprite;
    private PlayerCannon cannon;

    private void Start()
    {
        currentHealth = maxHealth;
        UpdateBar();

        move = GetComponent<PlayerMovement>();
        cannon = GetComponentInChildren<PlayerCannon>();
        playerSprite = GetComponent<SpriteRenderer>();
    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Y))
        {
            TakeDamage(1);
        }
    }

    public void TakeDamage(int dmg)
    {
        currentHealth -= dmg;

        if (currentHealth <= 0)
        {
            Die();
        }

        UpdateBar();
    }


    private void UpdateBar()
    {
        healthBar.value = (float)currentHealth / (float)maxHealth;
    }

    private void Die()
    {
        isDead = true;
        //playerSprite.enabled = false;
        move.enabled = false;
        cannon.gameObject.SetActive(false);

        Invoke(nameof(ResetGame), 2f);
    }

    private void ResetGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
