using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using JetBrains.Annotations;

public class Player_Controller : MonoBehaviour
{
    public static Player_Controller Instance;
    public int maxHealth = 100; //Salud máxima del jugador
    public int currentHealth;
    public int damage;

    Animator anim;
    Rigidbody2D rb;

    public float velocidad = 6;
    public float velocidadGuardar = 6;
    public GameObject ranged;
    public GameObject melee;
    public GameObject selectWeapon;
    public GameObject enemym;
    public GameObject wavem;
    public GameObject muerte;

    [Header("UI Settings")]
    public Image healthBarFill;
    public Transform healthBarCanvas;

    [Header("Audio Settings")]
    public AudioSource audioSource;   // Componente de AudioSource
    public AudioClip stepSound;       // Sonido de pasos
    public float stepInterval = 0.5f; // Intervalo entre cada paso
    private float nextStepTime = 0f;  // Control del tiempo entre pasos

    Vector2 direccionMovimiento;
    int facingDirection = 1;

    public GunController gunController;

    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();

        muerte.SetActive(false);
        gunController = FindObjectOfType<GunController>();
        SelectWeapon();
        currentHealth = maxHealth;
        UpdateHealthbar(maxHealth, currentHealth);

        if (audioSource == null)
        {
            audioSource = GetComponent<AudioSource>();
            if (audioSource == null)
                Debug.LogError("No se encontró un componente AudioSource en el jugador.");
        }
    }

    // Update is called once per frame
    void Update()
    {
        float moveX = Input.GetAxisRaw("Horizontal");
        float moveY = Input.GetAxisRaw("Vertical");

        direccionMovimiento = new Vector2(moveX, moveY).normalized;

        anim.SetFloat("Velocity", direccionMovimiento.magnitude);

        if (direccionMovimiento.magnitude > 0.1f)
        {
            PlayStepSound();
        }

        if (direccionMovimiento.x != 0)
        {
            facingDirection = direccionMovimiento.x > 0 ? 1 : -1;
        }
        transform.localScale = new Vector2(facingDirection, 1);
    }

    void FixedUpdate()
    {
        rb.velocity = new Vector2(direccionMovimiento.x * velocidad, direccionMovimiento.y * velocidad);
    }

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Debug.LogError("Se detectó más de un Player_Controller en la escena.");
            Destroy(gameObject); // Evita que haya múltiples instancias.
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        EnemyPlayer enemyp = collision.gameObject.GetComponent<EnemyPlayer>();
        if (enemyp != null)
        {
            Hit(20);
        }
    }

    void Hit(int damage)
    {
        anim.SetTrigger("Hit");
        currentHealth -= damage;
        UpdateHealthbar(maxHealth, currentHealth);

        Debug.Log("El jugador recibió daño");

        if (currentHealth <= 0)
        {
            Die();
            Time.timeScale = 0;
        }
    }

    public void Die()
    {
        Debug.Log("El jugador ha muerto.");
        muerte.SetActive(true);
    }

    public void IncreaseDamage(int value)
    {
        damage += value;
        Debug.Log("Nuevo Daño: " + damage);
    }

    public void IncreaseHealth(int value)
    {
        maxHealth += value;
        currentHealth += value;
        Debug.Log("Nueva Salud: " + maxHealth);
    }

    public void IncreaseAttackSpeed(float value)
    {
        Debug.Log("Atac");
        gunController.fireRate -= value;
        Debug.Log("Nuevo Velocidad de Ataque: " + gunController.fireRate);
    }

    public void SelectWeapon()
    {
        Time.timeScale = 0;
        selectWeapon.SetActive(true);
    }

    public void Ranged()
    {
        Time.timeScale = 1;
        ranged.SetActive(true);
        melee.SetActive(false);
        selectWeapon.SetActive(false);
        enemym.SetActive(true);
        wavem.SetActive(true);
    }

    public void Melee()
    {
        Time.timeScale = 1;
        ranged.SetActive(false);
        melee.SetActive(true);
        selectWeapon.SetActive(false);
        enemym.SetActive(true);
        wavem.SetActive(true);
    }

    public void UpdateHealthbar(float maxHealth, float currentHealth)
    {
        healthBarFill.fillAmount = currentHealth / maxHealth;
    }

    public void MovementSpeedIncrease(float value)
    {
        Debug.Log("MS");
        velocidad += value;
        velocidadGuardar += value;
        Debug.Log("Nueva velocidad de movimiento: " + velocidad);
    }

    void PlayStepSound()
    {
        if (Time.time >= nextStepTime)
        {
            audioSource.PlayOneShot(stepSound);
            nextStepTime = Time.time + stepInterval;
        }
    }

    // Paul Rutero.
}
