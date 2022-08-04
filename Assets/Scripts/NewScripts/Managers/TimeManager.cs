using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
public class TimeManager : Singleton<TimeManager>
{
    public float maxTime;
    public float currTime;

    public int Score;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ChangeTime(float timeChange)
    {
        currTime = Mathf.Clamp(currTime + timeChange, 0f, maxTime);

        if (currTime == 0)
        {
            //Call GameOver Event here!
        }
    }
}
