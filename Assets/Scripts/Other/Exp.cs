using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Exp : MonoBehaviour
{
    // Start is called before the first frame update
    public float stopTime;
    public float lifeTime;
    public float flickerTime;
    public float flickerTimeMult;
    public int Amount;
    public bool isFlickering = false;
    private Rigidbody2D rb2d;

    void Start()
    {
        print("I LIVE");
        stopTime = Ref.expTravelTime + Time.time;
        lifeTime = Ref.expLifetime + Time.time;
        flickerTime = (Ref.expLifetime * .75f) + Time.time;
        rb2d = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Time.time > stopTime)
        {
            rb2d.velocity = Vector2.zero;
        }
        if (!isFlickering && Time.time > flickerTime)
        {
            //Flicker here!
        }

        if (Time.time > lifeTime)
        {
            Destroy(gameObject);
            print("MyTimeHasCome");

        }
    
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.GetComponent<MainCharacter>())
        {
            collision.gameObject.GetComponent<MainCharacter>().GainEXP(Amount);
            Destroy(gameObject);
        }
    }
}
