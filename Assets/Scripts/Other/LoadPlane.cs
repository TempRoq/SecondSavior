using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoadPlane : MonoBehaviour
{
    private RoomBuilderManager rbm;
    // Start is called before the first frame update
    void Start()
    {
        rbm =  GetComponent<RoomBuilderManager>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.GetComponent<MainCharacter>())
        {
            print("LOADING NEW ROOM!");
            bool madeRoom = rbm.MakeNextRoom();
            if (!madeRoom)
            {
                Destroy(this);
            }
            GetComponent<BoxCollider2D>().offset = new Vector2(0, rbm.roomLengthInUnits * (rbm.nextRoomNumber - 1));
        }
    }
}
