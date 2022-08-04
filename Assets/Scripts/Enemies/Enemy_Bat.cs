using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_Bat : Enemy
{
    private Vector2 direc;

    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();
        direc = Vector2.left;
    }

    // Update is called once per frame
    protected override void Update()
    {
        base.Update();
    }
    public override void AI()
    {
        base.AI();

        if (PlayerTarget != null)
        {
            if (canFire)
            {
                Fire();
            }
        }

    }

    public override void OnSeePlayer()
    {
        rb2d.velocity = direc * speed;
        fireTime = Time.time;
    }


    protected override void OnCollisionEnter2D(Collision2D collision)
    {
        direc *= -1;
        rb2d.velocity = direc * speed;
        GetComponent<SpriteRenderer>().flipX = !GetComponent<SpriteRenderer>().flipX;
    }

}
