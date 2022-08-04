using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(menuName = "ActionCreator/Hitbox/Basic", order = 0) ]
public class Hitbox : ScriptableObject
{
    public int damage; 


    //All knockback and positions assume that the player is facing right.
    public Vector3 dimensions; 
    public Vector2 offsetFromAnchor;


    public Vector3 knockbackDirection;
    public float knockbackPower;

    public int framesHitstun;

    public virtual void OnHit(GameObject self, GameObject target) //Called when a hitbox successfully connects against a target. NOTE: Will not be called if the hit target is in "alreadyHit".
    {
  
    }
}
