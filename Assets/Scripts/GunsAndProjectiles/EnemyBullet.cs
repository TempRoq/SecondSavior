using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBullet : ProjectileParent
{
    protected override void OnTriggerEnter2D(Collider2D collision)
    {
        MainCharacter mc = collision.GetComponent<MainCharacter>();
        if (mc && !mc.invincible)
        {
            mc.TakeDamage(damage);
        }

        Destroy(gameObject);
    }
}
