using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_Skeleton : Enemy
{
    // Start is called before the first frame update
    public Vector2 distOffset;
    public float boomerangWaitTime;
    protected override void Start()
    {
        base.Start();
    }

    protected override void Update()
    {
        base.Update();
    }

    public override void AI()
    {
        base.AI();
        if (PlayerTarget != null && !inLag)
        {
            Vector3 v = PlayerTarget.transform.position + new Vector3(distOffset.x, distOffset.y);
            if (v.magnitude > 1f)
            {
                rb2d.velocity = ((PlayerTarget.transform.position + new Vector3(distOffset.x, distOffset.y)) - transform.position).normalized * speed;
            }
            else
            {
                rb2d.velocity = Vector2.zero;
            }
            if (canFire)
            {
                Fire();
            }
        }
    }

    public override void OnSeePlayer()
    {
        base.OnSeePlayer();
        Fire();
    }

    public override void Fire()
    {
        Vector3 dif = (PlayerTarget.transform.position - transform.position).normalized;
        print("nyehhhh!");
        gun.Fire(Mathf.Atan2(dif.y, dif.x) * Mathf.Rad2Deg, transform.position);
        fireTime = Time.time + timeBetweenShots;
        canFire = false;
        AddLag(boomerangWaitTime);
    }

    
}
