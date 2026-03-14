using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Menu_Pausa : MonoBehaviour
{
    public GameObject MenuPausa;
    public GameObject JoystickVirtual;
    public GameObject BotonPause;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape)) //El boton Escape (ESC) habre el menu de pausa
        {
            BotonPause.SetActive(false);
            MenuPausa.SetActive(true);
            JoystickVirtual.SetActive(false);
            Time.timeScale = 0f; 
        }
    }
    public void Pause() //Boton de Pausa
    {
        BotonPause.SetActive(false);
        MenuPausa.SetActive(true);
        JoystickVirtual.SetActive(false);
        Time.timeScale = 0f;
    }
    public void VolveralJuego() //Boton de volver al Juego desde el menu de Pausa
    {
        BotonPause.SetActive(true);
        MenuPausa.SetActive(false);
        JoystickVirtual.SetActive(true);
        Time.timeScale = 1f;
        
    }
    public void Reiniciar() //Boton de Reinicio de Juego
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        print("Funciona el reinicio");
    }

    public void Atras() //Boton de volver atras en el menu pause
    {
        MenuPausa.SetActive(true);
        Time.timeScale = 0f;
    }

    public void Menu()
    {
        SceneManager.LoadScene(0);
        Time.timeScale = 1f;
    }
}
