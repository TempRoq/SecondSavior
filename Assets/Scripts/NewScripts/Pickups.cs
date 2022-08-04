using System.Collections;
using System.Collections.Generic;
using UnityEngine;

#if UNITY_EDITOR
using UnityEditor;
#endif

[System.Serializable]
public class Pickups : MonoBehaviour
{
    public enum PickupType
    {
        GUN = 0,
        BULLET = 1,
        SECONDARY = 2
    }
    public bool giveGun;
    public bool giveBullet;
    public bool giveSecondary;

    [HideInInspector] public Gun gun;
    [HideInInspector]  public GameObject bullet;
    [HideInInspector]  public Action secondary;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        NewBaseMChar mc = collision.gameObject.GetComponent<NewBaseMChar>();
        if (mc != null)
        {
            if (giveGun)
            {
                mc.weapon = gun;
            }

            if (giveBullet)
            {
                mc.bullet = bullet;
            }

            if (giveSecondary)
            {
                mc.SecondaryAction = secondary;
            }
        }
        Destroy(gameObject);
    }

}

#if UNITY_EDITOR
[CustomEditor(typeof(Pickups))]
public class Pickups_Editor : Editor
{
    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();
        Pickups p = (Pickups)target;
        p.giveGun = EditorGUILayout.Toggle("Give Gun", p.giveGun);
        if (p.giveGun)
        {
            p.gun = EditorGUILayout.ObjectField("Gun", p.gun, typeof(Gun), true) as Gun;
        }

        p.giveBullet = EditorGUILayout.Toggle("Give Bullet", p.giveBullet);
        if (p.giveBullet)
        {
            p.bullet = EditorGUILayout.ObjectField("Bullet", p.bullet, typeof(GameObject), true) as GameObject;
        }

        p.giveSecondary = EditorGUILayout.Toggle("Give Secondary Ability", p.giveSecondary);
        if (p.giveSecondary)
        {
            p.secondary = EditorGUILayout.ObjectField("Secondary Ability", p.secondary, typeof(Action), true) as Action;
        }
    }

}

#endif