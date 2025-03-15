using UnityEngine;
using UnityEngine.UI;

public class EnemyPlayer : MonoBehaviour
{
   [SerializeField] int maxHealth = 100;
   [SerializeField] float speed = 2f;

    private int currentHealth;

    Animator anim;
    Transform target; // Seguir al objetivo.

    [Header("UI Settings")]
    public Image healthBarFill;
    public Transform healthBarCanvas;
    XpManager xpManager;

    void Start()
    {
        xpManager = FindObjectOfType<XpManager>();
        currentHealth = maxHealth;
        target = GameObject.Find("Player").transform;
        anim = GetComponent<Animator>();
        
        UpdateHealthbar(maxHealth, currentHealth);
    }

    private void Update()
    {
        if (target != null)
        {
            Vector3 direction = target.position - transform.position;
            direction.Normalize();

            transform.position += direction * speed * Time.deltaTime;

            var playerToTheRight = target.position.x > transform.position.x;
            transform.localScale = new Vector2(playerToTheRight ? -1 : 1, 1);
        }

    }
    public void UpdateHealthbar(float maxHealth, float currentHealth)
    {
        healthBarFill.fillAmount = currentHealth / maxHealth;
    }
    public void Hit(int damage)
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
        xpManager.xp += 20;
    }  
}
