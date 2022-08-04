using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpinAttack : MonoBehaviour
{
    // Start is called before the first frame update

    public int spinDamage;
    public float spinKnockback;
    public LayerMask HittableLayers;
    private float spinDuration;
    public bool hasSpinDuration = false;

    private List<GameObject> alreadyHit;
    private CircleCollider2D cc2d;
    private void Start()
    {
        cc2d = GetComponent<CircleCollider2D>();
        alreadyHit = new List<GameObject>();
        alreadyHit.Clear();
            
        
    }

    private void Update()
    {
        if (hasSpinDuration && Time.time > spinDuration)
        {
            gameObject.SetActive(false);
        }
    }


    private void OnEnable()
    {
        cc2d = GetComponent<CircleCollider2D>();
        alreadyHit = new List<GameObject>();
        alreadyHit.Clear();
        RaycastHit2D[] hurt = Physics2D.CircleCastAll(transform.position, cc2d.radius, Vector2.zero, 0f, HittableLayers);

        foreach(RaycastHit2D r in hurt)
        {
            Enemy e = r.collider.gameObject.GetComponent<Enemy>();
            if (e)
            {
                Vector2 kbg = (e.gameObject.transform.position - transform.root.position).normalized;
                print(kbg);

                e.TakeDamage(spinDamage, kbg, spinKnockback, true);
                if (e.ShouldBeDead())
                {
                    transform.root.gameObject.GetComponent<MainCharacter>().AddDashLag(0);

                }
            }

            alreadyHit.Add(r.collider.gameObject);
        }
    }


    private void OnDisable()
    {
        hasSpinDuration = false;
    }
    public void CheckSpinOriginal()
    {
        /*
        print("My name is genome soldier");
        float radius = GetComponent<CircleCollider2D>().radius;
        RaycastHit2D[] hit = Physics2D.CircleCastAll(gameObject.transform.position, radius, Vector2.zero, 0f, HittableLayers);
        foreach (RaycastHit2D r in hit)
        {
            Enemy e = r.collider.gameObject.GetComponent<Enemy>();
            if (e)
            {
                Vector2 kbg = (e.gameObject.transform.position - transform.root.position).normalized;

                e.TakeDamage(spinDamage, kbg, spinKnockback, true);
                if (e.ShouldBeDead())
                {
                    transform.root.gameObject.GetComponent<MainCharacter>().AddDashLag(0);

                }
            }
        }
        */
    }

    public void DisableMe(float duration)
    {
        spinDuration = Time.time + duration;
        hasSpinDuration = true;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        Enemy e = collision.GetComponent<Enemy>();
        if (e && !alreadyHit.Contains(collision.gameObject))
        {
            Vector2 kbg = (e.gameObject.transform.position - transform.root.position).normalized;
            print(kbg);
            e.TakeDamage(spinDamage, kbg, spinKnockback, true);
            if (e.ShouldBeDead())
            {
                transform.root.gameObject.GetComponent<MainCharacter>().AddDashLag(0);

            }

        }
    }
}
