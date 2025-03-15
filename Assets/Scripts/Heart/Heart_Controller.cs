using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Heart_Controller : MonoBehaviour
{
    public float bpm;
    public int damage;
    public int bpmCounter;
    public Player_Controller pc;
    public GunController gun;
    public AudioClip normalBeat;
    public AudioClip stunBeat;
    bool canBeat;
    bool canDmg;
    public int maxHealth = 1000;
    public int currentHealth;

    [Header("UI Settings")]
    public Image healthBarFill;
    public Transform healthBarCanvas;

    // Start is called before the first frame update
    void Start()
    {
        canDmg = true;
        canBeat = true;
        bpmCounter = 0;
        currentHealth = maxHealth;
        UpdateHealthbar(maxHealth, currentHealth);
        StartCoroutine(Beat());
    }

    // Update is called once per frame
    void Update()
    {
       
    }

    IEnumerator Beat()
    {
        if (bpmCounter >= 10)
        {
            canBeat = false;
            Debug.Log("Paralizado");
            gameObject.GetComponent<AudioSource>().PlayOneShot(stunBeat);
            StartCoroutine(Paralyze());
            bpmCounter = 0;
        }
        else if (canBeat)
        {
            Debug.Log("Latido");
            gameObject.GetComponent<AudioSource>().PlayOneShot(normalBeat);
            yield return new WaitForSeconds(bpm / 60);
            bpmCounter++;
        }
        else
        {
            yield return new WaitForSeconds(bpm / 60);
        }

        StartCoroutine(Beat());
    }

    IEnumerator Paralyze()
    {
        pc.velocidad = 0;
        gun.firedistance = 0;
        yield return new WaitForSeconds(1);
        canBeat = true;
        pc.velocidad = pc.velocidadGuardar;
        gun.firedistance = 10;
    }

    IEnumerator dmgCooldown()
    {
        canDmg = false;
        yield return new WaitForSeconds(2);
        canDmg = true;
    }
    public void UpdateHealthbar(float maxHealth, float currentHealth)
    {
        healthBarFill.fillAmount = currentHealth / maxHealth;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        EnemyHeart enemyp = collision.gameObject.GetComponent<EnemyHeart>();
        if (enemyp != null)
        {
            Hit(20);
        }
    }
    void Hit(int damage)
    {
        currentHealth -= damage;
        UpdateHealthbar(maxHealth, currentHealth);

        Debug.Log("El corazón recibió daño");

        if (currentHealth <= 0)
        {
            Time.timeScale = 0;
            pc.Die();
        }
    }
}