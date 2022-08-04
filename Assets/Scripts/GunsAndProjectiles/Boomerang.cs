using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boomerang : EnemyBullet
{
    // Start is called before the first frame update

    public float timer;
    public bool comingBack = false;
    protected override void Start()
    {
        timer = Time.time + (lifetime / 2);
        base.Start();
    }
    protected override void Update()
    {

        
        base.Update();
        if (Time.time > timer && !comingBack)
        {
            comingBack = true;
            DirectionFacing += 180;
        }
    }

    

    
}
