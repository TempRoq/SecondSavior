using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(menuName = "ActionCreator/Cluster/New Path")]
public class MoveCluster : HitboxCluster
{
    [SerializeField]
    Pathing.PathNode PathToFollow;
    // Start is called before the first frame update


    public override void OnStart(GameObject self)
    {
        base.OnStart(self);
        self.GetComponent<Pathing>().BeginNewShape(PathToFollow);
    }
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
