using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestBulletWithDonovan : EnemyBullet
{
    // Start is called before the first frame update
    float startTime;
    float t;
    protected override void Start()
    {
        base.Start();
        startTime = Time.time;
    }

    // Update is called once per frame
    protected override void Update()
    {
        t = Time.time - startTime;
        base.Update();
    }

    public override void GenMovePath()
    {
        DirectionFacing -= 1f;
        base.GenMovePath();

    }
}
