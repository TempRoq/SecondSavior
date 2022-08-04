using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using UnityEngine.UI;
public class RoomBuilderManager : MonoBehaviour
{
    public Vector3 startPos;
    public GameObject firstRoom;
    public GameObject secondRoom;
    public GameObject[] allRooms;
    public GameObject finalRoom;

    protected List<GameObject> roomPool;
    private GameObject[] spawnedRooms;
    

    public GameObject player;

    public int startSeed;

    public int length;
    public float roomLengthInUnits = 10.0f;

    public int nextRoomNumber = 2;

    public int rerollCost;

    public int spawnRoomIndex;

    Vector3 roomOffset;

    public AudioSource shuffle;

    public GameObject nextMapCanvasItem;

    private bool lastRoom = false;
    void Start()
    {

        lastRoom = false;
        spawnedRooms = new GameObject[length + 1];
        roomPool = new List<GameObject>();
        for(int i = 0; i < allRooms.Length; i++)
        {
            roomPool.Add(allRooms[i]);
        }
        roomOffset = new Vector3(0, roomLengthInUnits, 0);

        GameObject g = Instantiate(firstRoom);
        g.transform.position = startPos;
        g.GetComponent<Room>().Populate(g.transform.position);
        spawnedRooms[0] = g;

        GameObject g1 = Instantiate(secondRoom);
        g1.transform.position = startPos + roomOffset;
        g1.GetComponent<Room>().Populate(g1.transform.position);
        spawnedRooms[1] = g1;

    
        spawnRoomIndex = Random.Range(0, roomPool.Count);
        nextMapCanvasItem.GetComponent<Image>().sprite = roomPool[spawnRoomIndex].GetComponent<Room>().minimapInterpretation;
        print("new spawnRoomIndex = " + spawnRoomIndex);

        player = FindObjectOfType<MainCharacter>().gameObject;
       
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.R) && !player.GetComponent<MainCharacter>().invincible)
        {
            player.GetComponent<MainCharacter>().TakeDamage(rerollCost);
            shuffle.Play();
            RerollMap();
        }
    }

    public void RerollMap()
    {
        if (lastRoom)
        {
            return;
        }
        int i = spawnRoomIndex;
        spawnRoomIndex = Random.Range(0, roomPool.Count);
        if (i == spawnRoomIndex)
        {
            RerollMap();
        }
        nextMapCanvasItem.GetComponent<Image>().sprite = roomPool[spawnRoomIndex].GetComponent<Room>().minimapInterpretation;
        print("new spawnRoomIndex = " + spawnRoomIndex);
    }

    public void SetSeed(int seed)
    {
        Random.InitState(seed);
        startSeed = seed;
    }




    public bool MakeNextRoom()
    {
        if (nextRoomNumber == length-1)
        {
            print(finalRoom.name);
            print("Final room poggies");
            lastRoom = true;
            GameObject lG = Instantiate(finalRoom);
            lG.transform.position = startPos + (nextRoomNumber * roomOffset);
            lG.GetComponent<Room>().Populate(lG.transform.position);
            
            spawnedRooms[nextRoomNumber] = lG;

            nextMapCanvasItem.GetComponent<Image>().sprite = finalRoom.GetComponent<Room>().minimapInterpretation;
            return false;
        }
        if (nextRoomNumber > length)
        {
            print("damn bro nvm");
            return false;

        }
        GameObject nextRoom = roomPool[spawnRoomIndex];
        GameObject g = Instantiate(nextRoom);
        
        g.transform.position = startPos + (nextRoomNumber * roomOffset);
        g.GetComponent<Room>().Populate(g.transform.position);
        spawnedRooms[nextRoomNumber] = g;
        nextMapCanvasItem.GetComponent<Image>().sprite = spawnedRooms[nextRoomNumber].GetComponent<Room>().minimapInterpretation;
        roomPool.RemoveAt(spawnRoomIndex);

        if (nextRoomNumber - 3 >= 0)
        {
            Destroy(spawnedRooms[nextRoomNumber - 3]);
        }
        //if this doesn't work, try destroying its children (holy fuck that sounds dark)
        nextRoomNumber++;

        spawnRoomIndex = Random.Range(0, roomPool.Count);


        return true;
    }
}
