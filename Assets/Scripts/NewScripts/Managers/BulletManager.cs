using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletManager : Singleton<BulletManager>
{
    public int maxBulletsOnScreen = 4;
    public int playerBulletsOnScreen = 0;

    public override void OnAlreadyLoaded()
    {
        base.OnAlreadyLoaded();
        playerBulletsOnScreen = 0;
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

}
