using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SchmooveScript : MonoBehaviour
{
    // Start is called before the first frame update

    public GameObject player;
    public Slider s;
    public Sprite zero, one, two, three, four, five;
    Image image;

    void Start()
    {
        image = GetComponent<Image>();
        s.maxValue = player.GetComponent<MainCharacter>().expToLevelUp;
        s.value = player.GetComponent<MainCharacter>().exp;
        s.minValue = 0;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if(player.GetComponent<MainCharacter>().level == 0)
        {
            image.sprite = zero;
        }
        if (player.GetComponent<MainCharacter>().level == 1)
        {
            image.sprite = one;
        }
        if (player.GetComponent<MainCharacter>().level == 2)
        {
            image.sprite = two;
        }
        if (player.GetComponent<MainCharacter>().level == 3)
        {
            image.sprite = three;
        }
        if (player.GetComponent<MainCharacter>().level == 4)
        {
            image.sprite = four;
        }
        if (player.GetComponent<MainCharacter>().level == 5)
        {
            image.sprite = five;
        }

        s.value = player.GetComponent<MainCharacter>().exp;
    }
}
