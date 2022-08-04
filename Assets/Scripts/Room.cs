using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Room : MonoBehaviour
{
    public Sprite minimapInterpretation;
    public string levelName;
    public GameObject[] enemiesAndHazards;
    public Vector3[] allLocalPositions;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Populate(Vector3 position)
    {
        for(int i = 0; i <enemiesAndHazards.Length; i++)
        {
            GameObject g = Instantiate(enemiesAndHazards[i]);
            g.transform.position = allLocalPositions[i] + position;
        }
    }
}
