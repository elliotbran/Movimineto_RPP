using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunController : MonoBehaviour
{
    [Header("Prefavs")]
    [SerializeField] GameObject muzzle;
    [SerializeField] Transform muzzlePosition;
    [SerializeField] GameObject projectile;

    [Header("Config")]
    [SerializeField] public float firedistance = 10;
    [SerializeField] public float fireRate = 0.5f;

    [Header("Audio")]
    [SerializeField] AudioSource audioSource;     // Fuente de audio
    [SerializeField] AudioClip shootSound;        // Sonido del disparo

    Transform player;
    Vector2 offset;

    private float timeSinceLastShot = 0f;
    Transform closestEnemy;
    Animator anim;

    void Start()
    {
        anim = GetComponent<Animator>();
        timeSinceLastShot = fireRate;
        player = GameObject.Find("Player").transform;

        if (audioSource == null)
        {
            audioSource = GetComponent<AudioSource>();
            if (audioSource == null)
                Debug.LogError("No se encontró un componente AudioSource en el arma.");
        }
    }

    void Update()
    {
        transform.position = (Vector2)player.position + offset;
        FindClosestEnemy();
        AimAtEnemy();
        Shooting();
    }

    void FindClosestEnemy()
    {
        closestEnemy = null;
        EnemyPlayer[] enemyp = FindObjectsOfType<EnemyPlayer>();
        EnemyHeart[] enemyh = FindObjectsOfType<EnemyHeart>();

        List<Transform> allEnemies = new List<Transform>();

        foreach (EnemyHeart enemy in enemyh)
        {
            allEnemies.Add(enemy.transform);
        }

        foreach (EnemyPlayer enemy in enemyp)
        {
            allEnemies.Add(enemy.transform);
        }

        foreach (Transform enemy in allEnemies)
        {
            float distance = Vector2.Distance(transform.position, enemy.position);
            if (distance <= firedistance)
            {
                closestEnemy = enemy;
            }
        }
    }

    void AimAtEnemy()
    {
        if (closestEnemy != null)
        {
            Vector3 direction = closestEnemy.position - transform.position;
            direction.Normalize();

            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

            transform.rotation = Quaternion.Euler(0, 0, angle);
            transform.position = (Vector2)player.position + offset;
        }
    }

    void Shooting()
    {
        if (closestEnemy == null) return;

        timeSinceLastShot += Time.deltaTime;

        if (timeSinceLastShot >= fireRate)
        {
            Shoot();
            timeSinceLastShot = 0;
        }
    }

    void Shoot()
    {
        anim.SetTrigger("Attack");
        Debug.Log("Dispare");

        // Reproduce el sonido del disparo
        if (audioSource != null && shootSound != null)
        {
            audioSource.PlayOneShot(shootSound);
        }

        var muzzleGo = Instantiate(muzzle, muzzlePosition.position, transform.rotation);
        Destroy(muzzleGo, 0.05f);

        var projectileGo = Instantiate(projectile, muzzlePosition.position, transform.rotation);
        Destroy(projectileGo, 3);
    }
}
