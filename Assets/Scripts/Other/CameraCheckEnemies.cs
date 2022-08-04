using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraCheckEnemies : MonoBehaviour
{
    public float lerpRate;
    public LayerMask enemies;
    public Vector3 distanceFromPlayer;
    public int maxNumEnemiesOnScreen;
    private Camera cam;
    public bool screenLocked;
    private BoxCollider2D PlayerBlocker;
    private MainCharacter mc;
    private Vector3 startPos;

    float height;
    float width;
        
    // Start is called before the first frame update
    void Start()
    {
        cam = GetComponent<Camera>();
        PlayerBlocker = GetComponent<BoxCollider2D>();
        startPos = transform.position;
        mc = FindObjectOfType<MainCharacter>();
    }

    // Update is called once per frame
    void Update()
    {

        if (!screenLocked)
        {
            FollowPlayer();
        }
        height = cam.orthographicSize * 2f;
        width = cam.aspect * height;


        RaycastHit2D[] rc2d = Physics2D.BoxCastAll(transform.position, new Vector2(width, height), 0f, Vector2.zero, 0f, enemies);

        if (!screenLocked && rc2d.Length >= maxNumEnemiesOnScreen)
        {
            LockScreen();
            print("LOCK!");
        }

        else if (screenLocked && rc2d.Length == 0)
        {
            UnlockScreen();
            print("UNLOCK!");
        }
    }

    public void LockScreen()
    {
        screenLocked = true;
        
    }

    public void UnlockScreen()
    {
        screenLocked = false;
    }

    public void FollowPlayer()
    {
        Vector3 NewPosition = mc.transform.position + distanceFromPlayer;
        //transform.position = Vector3.Lerp(transform.position, new Vector3(startPos.x, NewPosition.y, startPos.z), lerpRate);
        if (NewPosition.y > gameObject.transform.position.y)
        {
            transform.position = Vector3.Lerp(transform.position, new Vector3(startPos.x, NewPosition.y, startPos.z), lerpRate * (mc.level + 1));

        }
        else
        {
            
            
        }
        
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        //Gizmos.DrawCube(cam.transform.position, new Vector2(width, height));

    }
}
