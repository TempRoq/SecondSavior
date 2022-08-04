using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[RequireComponent(typeof(Rigidbody2D))]
public class MainCharacter : MonoBehaviour
{
    Animator pAnim; 
    private Rigidbody2D rb2d;

    public Gun BasicFire;
    public int currentAmmo;

    [Header("Control booleans")]
    public bool canMove = true;
    public bool canDash = true;
    public bool canAttack = true;
    public bool dashing = false;
    public bool leaping = false;
    
    [Header("Dash")]
    public float dashDuration;
    public float dashForce;
    public float dashEndLag;
    public float dashInvincibilityTime;
    private float dashDurationTimer;

   

    [Header("Spin Attack")]
    public GameObject speenOrb;
    public float baseSpinDuration = .75f;
    public float spinEndlag;  
   

    private float canMoveAgain;
    private float canDashAgain;
    private float canAttackAgain;
    private float notInvincible;

    [Header("Invincibility")]
    public float invincibilityTimer = 0f;
    public bool invincible;

    [Header("Other")]
    public float levelMult;
    public float baseSpeed;
    public float slowSpeedMultiplier;
    public int level;
    public int maxLevel;
    public int exp;
    public int expToLevelUp;
    public Vector2 moveDir;
    public Vector3 dif;
    public float baseHitstun;
    public float baseKnockbackHit;
    public float hitInvin;


    public AudioSource shloop;

    public SpeedTimer timer;
    // Start is called before the first frame update
    void Start()
    {
        pAnim = gameObject.GetComponent<Animator>();
        rb2d = GetComponent<Rigidbody2D>();
        //Gun.Destroy?
    }

    // Update is called once per frame
    void Update()
    {
        dif = (Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position).normalized;
        if (dif.x > 0)
        {
            GetComponent<SpriteRenderer>().flipX = true;
        }
        else if (dif.x < 0)
        {
            GetComponent<SpriteRenderer>().flipX = false;
        }



        UpdateLag();
        CheckMovement();
        PerformMovement();


        //Dash
        if (Input.GetMouseButtonDown(0) && canDash)
        {
            Dash();
            pAnim.SetTrigger("Dash");
        }

        //Fire
        if (Input.GetMouseButtonDown(1) && canAttack)
        {
            float rot = Mathf.Atan2(dif.y, dif.x) * Mathf.Rad2Deg;
            if (rot < 0)
            {
                rot += 360;
            }
            BasicFire.Fire(rot, transform.position + ((Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position).normalized *.5f));
            if (dashing)
            {
                dashing = false;
               // rb2d.AddForce(-dif * BasicFire.kbSpeed);

            }
            //currentAmmo -= 1;
        }


        //Spin
        if (Input.GetKeyDown(KeyCode.Space) && canAttack)
        {
            pAnim.SetTrigger("Land");
            Spin();
        }

    }



    protected void UpdateLag()
    {
        if (!canMove && Time.time >= canMoveAgain)
        {
            canMove = true;
        }

        if (!canDash && Time.time >= canDashAgain)
        {
            canDash = true;
        }

        if (!canAttack && Time.time >= canAttackAgain)
        {
            canAttack = true;
        }

        if (invincible && Time.time >= notInvincible)
        {
            invincible = false;
            print("invin over.");
        }

        if (dashing && Time.time >= dashDurationTimer)
        {
            dashing = false;
            rb2d.velocity = Vector2.zero;
        }
    }
    public void Spin()
    {

        float lag = baseSpinDuration + spinEndlag;
        AddMoveLag(lag);
        AddDashLag(lag);
        AddAttackLag(lag);
        rb2d.velocity = Vector2.zero;
        speenOrb.gameObject.SetActive(true);
        speenOrb.GetComponent<SpinAttack>().DisableMe(baseSpinDuration);
       

    }
    //Took CheckMovement!
    public void CheckMovement()
    {
        moveDir = Vector2.zero;

        if (Input.GetKey(KeyCode.W))
        {
            moveDir.y += 1;
        }
        if (Input.GetKey(KeyCode.S))
        {
            moveDir.y -= 1;
        }
        if (Input.GetKey(KeyCode.A))
        {
            moveDir.x -= 1;
        }
        if (Input.GetKey(KeyCode.D))
        {
            moveDir.x += 1;
        }
        
        moveDir = moveDir.normalized;




        if (Input.GetKey(KeyCode.LeftShift))
        {
            moveDir *= slowSpeedMultiplier;
        }

    }
    //And PerformMovement
    public void PerformMovement()
    {
        if (canMove)
        {
            rb2d.velocity = baseSpeed * (1 + (levelMult * level)) * (moveDir);
        }
        
    }

    //Cutting Dash;
    public void Dash()
    {
        rb2d.velocity = Vector2.zero;
        speenOrb.SetActive(false);
        Vector2 newDirec = (Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position).normalized;

        rb2d.AddForce(newDirec.normalized * dashForce * (1 + (levelMult*level)), ForceMode2D.Impulse);
        AddDashLag(dashDuration + dashEndLag);
        AddMoveLag(dashDuration + dashEndLag);
        AddInvincibility(dashInvincibilityTime);
        dashDurationTimer = Time.time + dashDuration;
        dashing = true;
    }

    //Man fuck all that noise...
    public void AddDashLag(float time)
    {
        canDash = false;
        canDashAgain = Time.time + time;
    }
     
    public void AddMoveLag(float time)
    {
        canMove = false;
        canMoveAgain = Time.time + time;
    }
     
    public void AddAttackLag(float time)
    {
        canAttack = false;
        canAttackAgain = Time.time + time;
    }

    public void AddInvincibility(float time)
    {
        StartCoroutine(Flicker());
        invincible = true;
        notInvincible = Time.time + time;
    }


    public void GainEXP(int experienceGained)
    {
        
        exp += experienceGained;
        if (exp >= expToLevelUp)
        {
            int remainder = exp -  expToLevelUp;
            if (level < maxLevel)
            {
                level += 1;
                exp = 0;
                GainEXP(remainder);
                shloop.Play();

            }
            else
            {
                exp = expToLevelUp; //MAX EXP ACHIEVED
            }
        }
    }

    public void TakeDamage(int expLost)
    {
        if (invincible)
        {
            return;
        }
        else
        {
            exp -= expLost;
            if (exp < 0)
            {
                int remainder = -exp;
                if (level > 0)
                {
                    level -= 1;
                    exp = expToLevelUp - remainder;
                }
                else
                {
                    exp = 0;
                    //Call a die script here!
                    timer.Death();
                }
            }

            pAnim.SetTrigger("Hurt");
            speenOrb.SetActive(false);
            rb2d.velocity = Vector2.zero;
            AddDashLag(baseHitstun);
            AddMoveLag(baseHitstun);
            AddAttackLag(baseHitstun);
            AddInvincibility(baseHitstun + hitInvin);
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
            yield return new WaitForSeconds(invincibilityTimer / 12f);
        }
        c.a = 1;
        GetComponent<SpriteRenderer>().color = c;
    }


    public void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "Win")
        {
            timer.AAAAAAANDDD_TIME = true;
        }
    }
}
