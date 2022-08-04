using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class Enemy : MonoBehaviour
{
    public GameObject PlayerTarget;

    [Header("EXP")]
    public GameObject expObject;
    public int numExpDrop;   
    protected Rigidbody2D rb2d;

    [Space]
    [Header("Health")]
    protected int currentHealth;
    public int dashDieThreshold;
    public int maxHealth;
    public GameObject Healthbar;
    protected float HealthScale;
    [Space]
    [Header("Tracking")]
    public LayerMask lm;
    public Vector2 boxcastSize;
    public Vector2 boxcastOffset;
    [Space]
    [Header("Guns")]
    public Gun gun;
    public bool canFire;
    public float fireTime;
    public float timeBetweenShots;
    [Space]
    [Header("Misc.")]
    public float lagAmount;
    protected float lagTimer;
    public float speed;
    public bool inLag = false;
    public bool dashDie = false;
    public int contactDamage;
    public bool dealsTouchDamage;

    Animator anim;
    bool animated = false;
    public GameObject explosion;
    protected virtual void Start()
    {
        currentHealth = maxHealth;
        rb2d = GetComponent<Rigidbody2D>();
        HealthScale = Healthbar.transform.localScale.x;
        canFire = true;
        if (gameObject.GetComponent<Animator>())
        {
            anim = gameObject.GetComponent<Animator>();
            animated = true;
        }


        if (currentHealth <= dashDieThreshold)
        {
            dashDie = true;
            Healthbar.GetComponent<SpriteRenderer>().color = new Color(255, 0, 0);
        }
    }

    // Update is called once per frame

    public void AddLag()
    {
        lagTimer = Time.time + lagAmount;
        inLag = true;

    }

    public void AddLag(float lag)
    {
        lagTimer = Time.time + lag;
        inLag = true;
        rb2d.velocity = Vector2.zero;
    }

    public virtual void TakeDamage(int damage, Vector2 knockback, float force, bool lag)
    {
        currentHealth -= damage;
        if(animated)
            anim.SetTrigger("Hurt");
        float f = Mathf.Clamp((float)currentHealth / (float)maxHealth * HealthScale, 0, HealthScale);
        Healthbar.transform.localScale = new Vector3(f, Healthbar.transform.localScale.y, Healthbar.transform.localScale.z);
        if (currentHealth <= dashDieThreshold)
        {
            dashDie = true;
            Healthbar.GetComponent<SpriteRenderer>().color = new Color(255, 0, 0);
        }
        if (currentHealth <= 0)
        {
            currentHealth = 0;
            GameObject g = Instantiate(explosion, gameObject.transform);
            g.transform.localPosition = Vector3.zero;
            gameObject.GetComponent<SpriteRenderer>().enabled = false;
            gameObject.GetComponent<Collider2D>().enabled = false;
            Invoke("Die", .3f);
        }
        rb2d.velocity = Vector2.zero;
        rb2d.AddForce(knockback * force, ForceMode2D.Impulse);
        if (lag)
        {
            AddLag();
        }
    }

    public bool ShouldBeDead()
    {
        return (currentHealth <= 0);
    }

    public virtual void Die()
    {
        Vector2[] ret = new Vector2[numExpDrop];
        float angle = 360f / (float)numExpDrop;
        for (int i = 0; i < numExpDrop; i++)
        {
            ret[i] = new Vector2(Mathf.Cos(angle*i * Mathf.Deg2Rad), Mathf.Sin(angle*i * Mathf.Deg2Rad)).normalized;
            GameObject exp = Instantiate(expObject);
            exp.transform.position = gameObject.transform.position;
            exp.GetComponent<Rigidbody2D>().AddForce(ret[i] * Ref.expForce, ForceMode2D.Impulse);
        }
        Destroy(gameObject);
    }

    public virtual void DieSucc(GameObject p)
    {
        p.GetComponent<MainCharacter>().GainEXP(expObject.GetComponent<Exp>().Amount * numExpDrop);

        numExpDrop = 0;
        GameObject g = Instantiate(explosion, gameObject.transform);
        g.transform.localPosition = Vector3.zero;
        gameObject.GetComponent<SpriteRenderer>().enabled = false;
        gameObject.GetComponent<Collider2D>().enabled = false;

        Invoke("Die", .3f);
        //Destroy(gameObject);
    }

    protected virtual void Update()
    {
        if (!canFire && fireTime < Time.time)
        {
            canFire = true;
        }

        if (inLag && Time.time > lagTimer)
        {
            inLag = false;
            rb2d.velocity = Vector2.zero;
            if (PlayerTarget != null)
            {
                OnSeePlayer();
            }
        }

        if (!inLag)
        {
            AI();
            
        }

        

    }


    public virtual void Fire()
    {
        gun.Fire(0, transform.position);
        fireTime = Time.time + timeBetweenShots;
        canFire = false;
        if (animated)
            anim.SetTrigger("Attack");
    }



    public virtual void ScanForPlayer()
    {
        RaycastHit2D rc2d = Physics2D.BoxCast(new Vector3(boxcastOffset.x, boxcastOffset.y) + transform.position, boxcastSize, 0f, Vector2.zero, 0f, lm);
        if (rc2d && rc2d.collider.gameObject.GetComponent<MainCharacter>())
        {
            PlayerTarget = rc2d.collider.gameObject;
            OnSeePlayer();
        }
    }

    public virtual void AI()
    {
        if (PlayerTarget == null)
        {
            ScanForPlayer();
            
        }
    }

    protected virtual void OnTriggerEnter2D(Collider2D collision)
    {


        if (collision.gameObject.GetComponent<MainCharacter>() && dashDie && collision.gameObject.GetComponent<MainCharacter>().GetComponent<MainCharacter>().dashing)
        {
            collision.gameObject.GetComponent<MainCharacter>().AddDashLag(0);
            collision.gameObject.GetComponent<MainCharacter>().AddMoveLag(0);
            DieSucc(collision.gameObject);
            Instantiate(explosion, gameObject.transform);
        }


        
        if (dealsTouchDamage && collision.gameObject.GetComponent<MainCharacter>())
        {
            collision.gameObject.GetComponent<MainCharacter>().TakeDamage(contactDamage);
        }


}

    protected virtual void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.GetComponent<MainCharacter>() && dashDie && collision.gameObject.GetComponent<MainCharacter>().GetComponent<MainCharacter>().dashing)
        {
            DieSucc(collision.gameObject);
        }

    }


    private void OnDrawGizmos()
    {
        Gizmos.color = Color.black;
        Gizmos.DrawWireCube(new Vector3(boxcastOffset.x, boxcastOffset.y) + transform.position, boxcastSize);
    }

    public virtual void OnSeePlayer()
    {
    }
}
