using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewBaseMChar : Character
{

    public float baseSpeed;
    public float slowSpeedMultiplier;
    public float levelStartLength;
    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();
    }

    // Update is called once per frame
    protected override void Update()
    {
        CheckMovement();
        if (Input.GetKeyDown(KeyCode.J)) { TryAction(PrimaryAction); }
        else if (Input.GetKeyDown(KeyCode.K)) { TryAction(SecondaryAction); }

        if (invincible && invincibilityTimer >= Time.time)
        {
            invincible = false;
            inHitstun = false;
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            OnLevelStart();
        }
    }

    private void OnLevelStart()
    {
        invincible = false;
        canMove = false;
        canAttack = false;
        StartCoroutine(DoLevelIntro());
    }

    protected virtual IEnumerator DoLevelIntro()
    {
        invincible = true;
        invincibilityTimer = Time.time + levelStartLength;
        StartCoroutine(Flicker());
        rb2d.velocity = baseSpeed * Vector2.up * slowSpeedMultiplier;
        yield return new WaitForSeconds(levelStartLength);
        rb2d.velocity = Vector2.zero;
        yield return new WaitForSeconds(.25f);
        canMove = true;
        canAttack = true;

    }

    public void CheckMovement()
    {
        Vector2 moveDir = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical")).normalized;

        if (Input.GetKey(KeyCode.LeftShift))
        {
            moveDir *= slowSpeedMultiplier;
        }

        if (canMove)
        {
            rb2d.velocity = (baseSpeed) * (moveDir);
        }

    }

    public override void OnReceiverHit()
    {
        OnDirectHit(hbr.HitInfoReceived.justHitBy.damage);  
    }

    public override void OnDirectHit(int damage)
    {
        if (!invincible)
        {
            TimeManager.instance.ChangeTime(-damage);
            invincibilityTimer = Time.time + howLongInvincible;
            inHitstun = true;
            StartCoroutine(Flicker());
        }
    }
}
