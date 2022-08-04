using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_HomingWalk : Enemy
{
    // Start is called before the first frame update

    private float timeToFire;
    public float timeToFireAgain;

    protected override void Start()
    {
        base.Start();
    }

    protected override void Update()
    {
        base.Update();
       
    }

    protected override void OnTriggerEnter2D(Collider2D collision)
    {
        base.OnTriggerEnter2D(collision);
    }

    public override void OnSeePlayer()
    {
        base.OnSeePlayer();
        Fire();
    }

    public override void AI()
    {
        base.AI();
        if (PlayerTarget!= null)
        {
            rb2d.velocity = (PlayerTarget.transform.position - transform.position).normalized * speed;
            if (canFire)
            {
                Fire();
            }
        }
    }

    public override void Fire()
    {
        Vector3 dif = (PlayerTarget.transform.position - transform.position).normalized;

        gun.Fire(Mathf.Atan2(dif.y, dif.x) * Mathf.Rad2Deg, transform.position);
        fireTime = Time.time + timeBetweenShots;
        canFire = false;
    }
}
