using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneManagemer : MonoBehaviour
{

    public GameObject Transition;

    string theLevel = "Title";

    /*
    public void Update()
    {
        if (Input.GetKeyDown(KeyCode.Backspace))
        {
            Animator tranimator;
            tranimator = Transition.GetComponent<Animator>();
            tranimator.SetTrigger("Down");
            
            Invoke("ActuallyChangeScene", 1.3f);
        }
    }
    */

    public void TitleScreen(string level)
    {
        theLevel = level;

        /*Animator tranimator;
        tranimator = Transition.GetComponent<Animator>();
        tranimator.SetTrigger("Down");*/
        Invoke("ActuallyChangeScene", 2f);
    }


    public void ChangeScene(string level)
    {
        theLevel = level;

        Animator tranimator;
        tranimator = Transition.GetComponent<Animator>();
        tranimator.SetTrigger("Down");

        Invoke("ActuallyChangeScene", 1f);
    }

    void ActuallyChangeScene()
    {
        SceneManager.LoadScene(theLevel);
    }


    public void QuitGame()
    {
        Application.Quit();
    }
}
