using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class SeedStorage : MonoBehaviour
{


    public string seed;
    int seedint;
    public InputField Sneed;

    public void Awake()
    {
        if(SceneManager.GetActiveScene().name == "Title")
            DontDestroyOnLoad(gameObject);

    }

    public void SetSeed()
    {
        seed = Sneed.text;
        seedint = int.Parse(seed);
        Random.InitState(seedint);
    }
}
