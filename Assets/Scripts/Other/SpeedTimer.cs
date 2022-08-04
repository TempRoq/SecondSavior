using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;


public class SpeedTimer : MonoBehaviour
{

    public float GameOverTime;
    public GameObject LoserStuff;
    public GameObject topbutton;

    public GameObject Winner;
    
    public Text MainTimer;
    public Text Split1;
    public Text Split2;

    float timeamount = 0.00f;

    public bool FirstSplit = false;
    public bool SecondSplit = false;
    public bool AAAAAAANDDD_TIME = false;

    // Start is called before the first frame update
    void Awake()
    {
        Time.timeScale = 1;
        //DontDestroyOnLoad(gameObject);
        MainTimer.text = timeamount.ToString();

        
    }

    // Update is called once per frame
    void Update()
    {
        if (!AAAAAAANDDD_TIME)
        {
            timeamount += Time.deltaTime;
            //timeamount = (Mathf.Round(timeamount * 1000))/1000f;
            MainTimer.text = timeamount.ToString();
        }

        if (AAAAAAANDDD_TIME)
        {
            Winner.SetActive(true);
            Time.timeScale = 0;
            AAAAAAANDDD_TIME = false;
        }

        if (FirstSplit)
        {
            Split1.text = timeamount.ToString();
            FirstSplit = false;
        }

        if (SecondSplit)
        {
            Split2.text = timeamount.ToString();
            SecondSplit = false;
        }




        if (timeamount >= GameOverTime )
        {
            print("you suck lmoa");
            LoserStuff.SetActive(true);
            var eventS = EventSystem.current;
            eventS.SetSelectedGameObject(topbutton, new BaseEventData(eventS));
            Time.timeScale = 0;
        }
    }


    public void ResetTime()
    {
        Time.timeScale = 1;
    }


    public void Death()
    {
        print("you suck lmoa");
        LoserStuff.SetActive(true);
        var eventS = EventSystem.current;
        eventS.SetSelectedGameObject(topbutton, new BaseEventData(eventS));
        Time.timeScale = 0;
    }
}
