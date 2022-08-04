using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileParent : MonoBehaviour
{
    // Start is called before the first frame update
    public float DirectionFacing = 0f;
    public float speed;
    public float lifetime = 1f;
    public int damage;
    public float knockbackForce;
    protected Rigidbody2D rb2d;

    public virtual void GenMovePath() //Should always go after the child's GenMovePath override
    {
        float xc = Mathf.Cos(DirectionFacing * Mathf.Deg2Rad);
        float yc = Mathf.Sin(DirectionFacing * Mathf.Deg2Rad);
        gameObject.transform.eulerAngles = new Vector3(transform.eulerAngles.x, transform.eulerAngles.y, DirectionFacing);
        rb2d.velocity = new Vector2(xc, yc) * speed;
    }
    protected virtual void Start()
    {
        lifetime = lifetime + Time.time;
        rb2d = GetComponent<Rigidbody2D>();
        transform.rotation = Quaternion.Euler(0f, 0f, 0f);
    }

    public virtual void SetAngle(float angle)
    {
        DirectionFacing = angle;
        if (DirectionFacing < 0)
        {
            DirectionFacing += 360;
        }
        gameObject.transform.eulerAngles = new Vector3(transform.eulerAngles.x, transform.eulerAngles.y, DirectionFacing);
    }


    protected virtual void Update()
    {
        if (lifetime < Time.time)
        {
            Destroy(gameObject);
        }

        GenMovePath();
    }

    protected virtual void OnTriggerEnter2D(Collider2D collision)
    {
        print("Kaboom!");
        Destroy(gameObject);


        
    }

    protected virtual void OnDestroy()
    {


    }

}
