using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SeeBox : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.GetComponent<MainCharacter>())
        {
            transform.parent.gameObject.GetComponent<Enemy>().PlayerTarget = collision.gameObject;
        }
        Destroy(gameObject);
    }
}
