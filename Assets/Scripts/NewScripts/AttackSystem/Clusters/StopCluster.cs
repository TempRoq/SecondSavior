using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(menuName = "ActionCreator/Cluster/Stop", order = 0)]
public class StopCluster : HitboxCluster
{
    // Start is called before the first frame update
    public override void OnStart(GameObject self)
    {
        base.OnStart(self);
        self.GetComponent<Pathing>().PausePathing();
    }
}
