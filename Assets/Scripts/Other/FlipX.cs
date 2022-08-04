using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlipX : MonoBehaviour
{
    Rigidbody2D rb2d;
    SpriteRenderer sr;

    public bool startedFacingLeft;
    void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
        sr = GetComponent<SpriteRenderer>();
    }



    // Update is called once per frame
    void Update()
    {
        if (rb2d.velocity.x > 0)
        {
            if (startedFacingLeft)
            {
                sr.flipX = true;
            }
            else
            {
                sr.flipX = false;
            }
        }
        

        else if (rb2d.velocity.x < 0)
        {
            if (startedFacingLeft)
            {
                sr.flipX = false;
            }
            else
            {
                sr.flipX = true;
            }
        }
    }
}
