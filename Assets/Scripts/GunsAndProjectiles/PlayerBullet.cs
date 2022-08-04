using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBullet : ProjectileParent
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

    public override void GenMovePath()
    {

        base.GenMovePath();
    }

    protected override void OnDestroy()
    {
        BulletManager.instance.playerBulletsOnScreen -= 1;
        if (BulletManager.instance.playerBulletsOnScreen < 0)
        {
            print("hey hey, that shouldn't happen!");
            BulletManager.instance.playerBulletsOnScreen = 0;
        }
    }
}
