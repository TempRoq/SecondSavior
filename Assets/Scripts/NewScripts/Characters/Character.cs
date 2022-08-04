using System.Collections;
using System.Collections.Generic;
using UnityEngine;


//[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(ActionHandler))]
[RequireComponent(typeof(HitboxReceiver))]
public class Character : MonoBehaviour
{




    protected Rigidbody2D rb2d;
    protected HitboxReceiver hbr;
    protected ActionHandler ah;

    public Action PrimaryAction;
    public Action SecondaryAction;

    public Gun weapon;
    public GameObject bullet;

    
    public float baseDamage;


    [Header("Control booleans")]

    protected float hitstunTimer;
    protected float lagTimer;
    protected float invincibilityTimer;

    public float howLongInvincible;
    public bool canMove = true;
    public bool canAttack = true;
    public bool invincible = false;
    public bool inHitstun = false;

    // Start is called before the first frame update
    protected virtual void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
        ah = GetComponent<ActionHandler>();
        hbr = GetComponent<HitboxReceiver>();
        hbr.OnHit.AddListener(OnReceiverHit);
    }

   

   
    // Update is called once per frame
    protected virtual void Update()
    {     
        
    }

    

    public virtual void OnReceiverHit()
    {
       
    }

    public virtual void OnDirectHit(int damage)
    {

    }

    public void TryAction(Action a)
    {
        if ((lagTimer <= Time.time) && canAttack)
        {
            ah.PerformAction(a);
            lagTimer = Time.time + a.attackLagFrames / 60f;
        }
    }

    public IEnumerator Flicker()
    {
        bool flick = false;
        Color c = GetComponent<SpriteRenderer>().color;
        while (invincible)
        {
            if (flick)
            {
                c.a = .5f;
                GetComponent<SpriteRenderer>().color = c;
                flick = false;
            }
            else
            {
                c.a = 1f;
                GetComponent<SpriteRenderer>().color = c;
                flick = true;
            }
            yield return new WaitForSeconds(5/60f);
        }
        c.a = 1;
        GetComponent<SpriteRenderer>().color = c;
    }
}
