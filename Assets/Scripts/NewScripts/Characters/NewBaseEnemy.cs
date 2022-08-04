using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewBaseEnemy : Character
{
    public int pointValue;
    public int health;
    protected Color colorBase;
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

    public override void OnDirectHit(int damage)
    {
        print("I got hurt!");
        print(damage + " = damage done");
        health -= damage;
        print(health + " = new health");
        if (health <= 0)
        {
            Destroy(gameObject);
        }

    }

    public override void OnReceiverHit()
    {
        base.OnReceiverHit();
    }

    private void OnDestroy()
    {
        
    }
}
