using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ButtonController : MonoBehaviour
{
    public GameObject Fade;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void PlayButton()
    {
        StartCoroutine(Play());
    }

    public void Quit()
    {
        Application.Quit();
    }

    IEnumerator Play()
    {
        Fade.SetActive(true);
        Fade.gameObject.GetComponent<Animator>().Play("FadeOut");
        yield return new WaitForSeconds(1);
        SceneManager.LoadScene(1);
    }
}
