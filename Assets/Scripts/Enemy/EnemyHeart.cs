using UnityEngine;
using UnityEngine.UI;
using static UnityEngine.GraphicsBuffer;

public class EnemyHeart : MonoBehaviour
{
    public int maxHealth = 50;
    public float moveSpeed;
   [SerializeField] private float currentHealth;

    Transform heart;

    [Header("UI Settings")]
    public Image healthBarFill;
    public Transform healthBarCanvas;
    XpManager xpManager;

    void Start()
    {
        xpManager = FindObjectOfType<XpManager>();
        currentHealth = maxHealth;
        heart = GameObject.Find("Heart").transform;
        UpdateHealthbar(maxHealth, currentHealth);
    }

    private void Update()
    {
        if (heart != null)
        {
            Vector3 direction = heart.position - transform.position;
            direction.Normalize();

            transform.position += direction * moveSpeed * Time.deltaTime;

            var heartDirection = heart.position.x > transform.position.x;
            transform.localScale = new Vector2(heartDirection ? -1 : 1, 1);
        }

        transform.position = Vector2.MoveTowards(transform.position, heart.transform.position, moveSpeed * Time.deltaTime); // Mover el enemigo hacia el jugador constantemente.
    }

    public void UpdateHealthbar(float maxHealth, float currentHealth)
    {
        healthBarFill.fillAmount = currentHealth / maxHealth;
    }
    public void Hit(float damage)
    {
        currentHealth -= damage;
        UpdateHealthbar(maxHealth, currentHealth);

        Debug.Log($"El enemigo recibió {damage} de daño. Salud restante: {currentHealth}");

        if (currentHealth <= 0)
        {
            Die();
        }
    }
    void Die()
    {
        Debug.Log("El enemigo ha muerto.");
        Destroy(gameObject);
    }
}
