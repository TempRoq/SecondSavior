using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "ActionCreator/Action/FollowPath")]
public class PathAction : Action
{
    // Start is called before the first frame update
    [SerializeField]
    Pathing.Path pathToFollow;

    public override void OnInvoked(GameObject self)
    {
        base.OnInvoked(self);
        self.GetComponent<Pathing>().BeginPath(pathToFollow);
    }
}
