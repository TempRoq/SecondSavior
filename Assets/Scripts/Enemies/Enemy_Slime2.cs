using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_Slime2 : Enemy_Slime
{
    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();
    }

    // Update is called once per frame
    protected override void Update()
    {
        base.Update();
    }

    public override void AI()
    {
        rb2d.velocity = Vector2.down * speed;
    }
}
