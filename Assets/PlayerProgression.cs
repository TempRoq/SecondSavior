using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerProgression : MonoBehaviour
{

    Slider Progression;

    public RoomBuilderManager room;

    public float NumOfLevels;
    public float CurrentLevel;

    public SpeedTimer splits;
    // Start is called before the first frame update
    void Start()
    {
        NumOfLevels = room.length;
        Progression = gameObject.GetComponent<Slider>();
    }

    // Update is called once per frame
    void Update()
    {
        CurrentLevel = room.nextRoomNumber - 1;

        Progression.value = CurrentLevel * (100 / NumOfLevels);

        if (CurrentLevel == 5)
            splits.FirstSplit = true;
        if (CurrentLevel == 10)
            splits.SecondSplit = true;
        //if (CurrentLevel == NumOfLevels)
            //splits.AAAAAAANDDD_TIME = true;

    }
}
