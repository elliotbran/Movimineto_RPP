using UnityEngine;

public class AutoAttack : MonoBehaviour
{
    [Header("Attack Settings")]
    public float attackRange = 3.0f;         // Rango de ataque
    public float attackCooldown;            // Tiempo entre ataques
    public int damage;                      // Daño por ataque

    [Header("References")]
    public LayerMask enemyLayer;            // Capa de enemigos
    public Transform attackPoint;           // Punto desde donde se realiza el ataque (opcional)
    public AudioClip swordSound;            // Sonido de espada
    public AudioSource audioSource;         // Componente de AudioSource

    private float nextAttackTime = 0f;

    public Player_Controller controller;
    public GunController gunController;

    Animator anim;

    private void Start()
    {
        anim = GetComponent<Animator>();
        controller = FindObjectOfType<Player_Controller>();
        gunController = FindObjectOfType<GunController>();

        // Asegúrate de que el AudioSource esté configurado
        if (audioSource == null)
        {
            audioSource = gameObject.AddComponent<AudioSource>();
        }
    }

    void Update()
    {
        attackCooldown = gunController.fireRate;
        damage = controller.damage * 2;
        AutoAttackEnemy();
    }

    void AutoAttackEnemy()
    {
        if (Time.time >= nextAttackTime)
        {
            Collider2D[] enemiesInRange = Physics2D.OverlapCircleAll(transform.position, attackRange, enemyLayer);
            if (enemiesInRange.Length > 0)
            {
                Transform nearestEnemy = GetNearestEnemy(enemiesInRange);
                if (nearestEnemy != null)
                {
                    Attack(nearestEnemy);
                    nextAttackTime = Time.time + attackCooldown;
                }
            }
        }
    }

    Transform GetNearestEnemy(Collider2D[] enemies)
    {
        Transform nearestEnemy = null;
        float shortestDistance = Mathf.Infinity;

        foreach (Collider2D enemy in enemies)
        {
            float distanceToEnemy = Vector2.Distance(transform.position, enemy.transform.position);
            if (distanceToEnemy < shortestDistance)
            {
                shortestDistance = distanceToEnemy;
                nearestEnemy = enemy.transform;
            }
        }

        return nearestEnemy;
    }

    void Attack(Transform enemy)
    {
        
        audioSource.PlayOneShot(swordSound, 0.3f);
        anim.SetTrigger("Attack");
        Debug.Log("Atacando a " + enemy.name);

        EnemyPlayer enemypHealth = enemy.GetComponent<EnemyPlayer>();
        EnemyHeart enemyhHealth = enemy.GetComponent<EnemyHeart>();

        if (enemypHealth != null)
        {
            enemypHealth.Hit(damage);
        }
        else if (enemyhHealth != null)
        {
            enemyhHealth.Hit(damage);
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, attackRange);
    }
}
