using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "ActionCreator/Cluster/FireOwnGun")]

public class FireCluster : HitboxCluster
{
    // Start is called before the first frame update
    public override void OnStart(GameObject self)
    {

        Debug.Log("Firing cluster!");
        Character c = self.GetComponent<Character>();
        if (c.weapon.Projectiles.Length + BulletManager.instance.playerBulletsOnScreen <= BulletManager.instance.maxBulletsOnScreen)
        { 
            c.weapon.Fire(90, self.transform.position, c.bullet);
            base.OnStart(self);
            BulletManager.instance.playerBulletsOnScreen += c.weapon.Projectiles.Length;
        }

        else
        {
            Debug.Log("Sorry, that'd be too many bullets");
        }
        
    }
}
