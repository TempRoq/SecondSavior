using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Gun")]
public class Gun : ScriptableObject
{

    public GameObject[] Projectiles;
    public float[] startAngleOffsets;

   // public int maxOnScreen;

    public virtual void Fire(float startAngle, Vector3 barrelPosition)
    {
        for(int i = 0; i < Projectiles.Length; i++)
        {
            GameObject g = Instantiate(Projectiles[i]);
            g.transform.position = barrelPosition;
            g.GetComponent<ProjectileParent>().SetAngle(startAngle + startAngleOffsets[i]);

            //print(startAngle + startAngleOffsets[i]);

        }
    }

    public virtual void Fire(float startAngle, Vector3 barrelPosition, GameObject newProjectiles)
    {
        for (int i = 0; i < startAngleOffsets.Length; i++)
        {
            GameObject g = Instantiate(newProjectiles);
            g.transform.position = barrelPosition;
            g.GetComponent<ProjectileParent>().SetAngle(startAngle + startAngleOffsets[i]);

            //print(startAngle + startAngleOffsets[i]);

        }



    }


}
