using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManag : MonoBehaviour
{
    [SerializeField] GameObject enemyprefab;
    [SerializeField] GameObject enemyprefab2;
    [SerializeField] GameObject enemyprefab3;
    [SerializeField] GameObject enemyprefab4;
    [SerializeField] public float timebspawns = 3.5f; //Cada cuanto spawnean
    [SerializeField] public int enemynumer = 1; //numero de enemigos por tick
    [SerializeField] public int minenemies = 0; //minimo de enemigos por grupo
    [SerializeField] public int maxenemies = 3; //maximo de enemigos por grupo
    float currenttimespawns;
    Transform enemyparent;
    public static EnemyManag Instance;

    private void Awake()
    {
        if (Instance == null) Instance = this;
    }

    void Start()
    {
        enemyparent = GameObject.Find("enemies").transform;//donde se guardan los enemigos
    }


    void Update()
    {
        if (!WaveManag.Instance.waverunning()) return;//comprueba si la oleada esta activa

        currenttimespawns -= Time.deltaTime;

        if (currenttimespawns <= 0)// cuenta para spawnear enemigos
        {
            spawnenemy();
            currenttimespawns = timebspawns;
        }
    }

    Vector2 RandomPosition()
    {
        return new Vector2(Random.Range(-12, -10), Random.Range(-6, 5));//Rango en el que spawnean enemigos
    }
    Vector2 RandomPosition2()
    {
        return new Vector2(Random.Range(12, 10), Random.Range(6, -5));//Rango en el que spawnean enemigos
    }


    void spawnenemy()//Funcion de spawn de enemigos
    {
        enemynumer = Random.Range(minenemies, maxenemies);
        for (int i = 1; i <= enemynumer; i++)
        {
            var Roll = Random.Range(0, 100);
            var spawnp = Roll < 50 ? RandomPosition() : RandomPosition2();// spawnean a la derecha o a la izquierda
            var enemytype = Roll < 50 ? enemyprefab : enemyprefab3;// spawn de varios enemigos
            var enemytype2 = Roll < 50 ? enemyprefab2 : enemyprefab4;
            var reRoll = Random.Range(0, 100);
            var finalenemy = reRoll < 50 ? enemytype2 : enemytype;
            var e = Instantiate(finalenemy, spawnp, Quaternion.identity);
            e.transform.SetParent(enemyparent);
        }
    }
    public void destroyallenemies()//para destruir a todos los enemigos de la escena
    {
        foreach (Transform e in enemyparent)
            Destroy(e.gameObject);
    }
}
