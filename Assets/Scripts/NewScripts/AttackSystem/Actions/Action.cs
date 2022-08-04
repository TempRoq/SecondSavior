using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

/*
 * 
 * FMOD.Studio.EventInstance mineSFX;


void whatever()
mineSFX = FMODUnity.RuntimeManager.CreateInstance("event:/SFX/MineHit");

        mineSFX.start();
        mineSFX.release();
 */



[CreateAssetMenu (menuName = "ActionCreator/Action/Basic", order = 2)]
public class Action : ScriptableObject
{
    public HitboxCluster[] clusters; //All clusters. Order matters.
    public int[] clusterFrames; // The frame (assuming 60fps) when a cluster at the same index in clusters is called
    public int attackLagFrames; //How long from the beginning of the move until the player can move again
    public bool stopUser; //Should all player movement be halted when this move is called?
   // public bool playInvokeSFX;
   // public string sfxFileName;    //FMOD.Studio.EventInstance invokeSFX;

    public virtual void OnInvoked(GameObject self) //Attack handler calls this on the first frame that the move is active (Frame 0)
    {/*
        if (playInvokeSFX)
        {
            invokeSFX = FMODUnity.RuntimeManager.CreateInstance("event:/SFX/" + sfxFileName);
            invokeSFX.start();
            invokeSFX.release();
            Print(I am invoked);
        }
        */
    }
}
