using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : PlayerBullet
{
    // Start is called before the first frame update


    protected override void OnTriggerEnter2D(Collider2D collision)
    {
        Character c = collision.gameObject.GetComponent<Character>();
        if (c != null)
        {
            c.OnDirectHit(damage);
        }
        base.OnTriggerEnter2D(collision);
    }
}
