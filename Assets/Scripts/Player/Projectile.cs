using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    EnemyPlayer enemyp;
    EnemyHeart enemyh;
    Player_Controller controller;
    float speed = 40f;
    // Start is called before the first frame update
    void Start()
    {
        controller = FindObjectOfType<Player_Controller>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        transform.Translate(Vector2.right * speed * Time.deltaTime);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        var enemyp = collision.gameObject.GetComponent<EnemyPlayer>();
        var enemyh = collision.gameObject.GetComponent<EnemyHeart>();
        var currentDmg = controller.damage;
        if (enemyp != null)
        {
            Destroy(gameObject);
            enemyp.Hit(currentDmg); //activa la funcion de takedmg y le quita 20 puntos de vida al enemigo
            Debug.Log("Le has dado al enemigo");
        }
        else if (enemyh != null) 
        {
            Destroy(gameObject);
            enemyh.Hit(currentDmg);
            Debug.Log("Le diste a un tanque");
        }
    }
}
