using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScannerBox : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.GetComponent<MainCharacter>())
        {
            gameObject.transform.root.gameObject.GetComponent<Enemy>().PlayerTarget = collision.gameObject;
        }
    }
}
