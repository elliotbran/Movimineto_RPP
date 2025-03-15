using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using static GameManager;

public class WaveManag : MonoBehaviour
{

    [SerializeField] TextMeshProUGUI timetx;//textos que se ven por pantalla
    [SerializeField] TextMeshProUGUI wavetx;

    public static WaveManag Instance;
    public static EnemyManag EnemyManag;
    public GameObject cartas;
    GameManager gameManager;
    Player_Controller playerController;
    Heart_Controller heartControll;


    public bool waverun = true;//si la oleada esta activa
    int currentwave = 0;//oleada actual
    int currentwavet;//tiempo de la oleada actual

    private void Awake()
    {
        if (Instance == null) Instance = this;
    }

    private void Start()
    {
        playerController = FindObjectOfType<Player_Controller>();
        gameManager = FindObjectOfType<GameManager>();
        EnemyManag = FindObjectOfType<EnemyManag>();
        startnwave();//comineza la primera oleada

    }
    private void Update()
    {
        if (waverun == false)
        {

           // if (Input.GetKeyDown(KeyCode.Space))//comenzar la siguiente oleada
           // {
//
           //     startnwave();
           //   
           // }
        }

    }

    public bool waverunning() => waverun;

    public  void startnwave()//funcion de nueva oleada
    {
        StopAllCoroutines();
        timetx.color = Color.white;
        currentwave++;
        waverun = true;
        currentwavet = 30;
        wavetx.text = "WAVE: " + currentwave;
        if (EnemyManag.timebspawns > 1f)//tiempo minimo en el que los enemigos spawnearan
        {
            EnemyManag.timebspawns -= 0.1f;//cunto tiempo resta por cambio de oleada
        }
        if (EnemyManag.minenemies < 4)//grupo minimo del rango de spawn
        {
            EnemyManag.minenemies++;//agrega 1 al rango de spawn cada ronda (hasta 5)
        }
        if (EnemyManag.maxenemies < 5)//grupo maximo del rango de spawn
        {
            EnemyManag.maxenemies++;//agrega 1 al rango de spawn cada ronda (hasta 10)
        }
        StartCoroutine(Wavetimer());
    }
    IEnumerator Wavetimer()//cuenta los 30 segundos de la oleada
    {
        while (waverun)
        {
            yield return new WaitForSeconds(1f);
            currentwavet--;

            timetx.text = currentwavet.ToString();

            if (currentwavet <= 0)
            wavecomplete();
        }

       
        yield return null;
    }

    private void wavecomplete()//se acaba la oleada
    {     
        playerController.currentHealth = playerController.maxHealth;
        playerController.UpdateHealthbar(playerController.maxHealth, playerController.currentHealth); 
        cartas.SetActive(true);
        StopAllCoroutines();
        EnemyManag.Instance.destroyallenemies();
        waverun = false;
        currentwavet = 30;
        timetx.text = currentwavet.ToString();
        gameManager.ChangeState(GameState.CardSelection);
        timetx.color = Color.red;
    }
}
