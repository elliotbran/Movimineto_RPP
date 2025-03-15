using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Menu_Controller : MonoBehaviour
{
    public GameObject loadScreen;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public IEnumerator ChangeScene()
    {
        //Cargar una escena
        loadScreen.SetActive(true);
        loadScreen.GetComponent<Animator>().Play("loadscreen");
        yield return new WaitForSeconds(9.3f);
        SceneManager.LoadScene(1);
    }
    public void ExitGame()
    {
        Debug.Log("Salir");
        Application.Quit();
    }
    public void cambiodpantalla()
    {
        StartCoroutine(ChangeScene());
    }

}
