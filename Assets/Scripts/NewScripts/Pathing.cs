using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D), typeof(ActionHandler))]
public class Pathing : MonoBehaviour
{

    public GameObject anchorObject;
    public Action pathingAction;
    protected Rigidbody2D rb2d;
    protected ActionHandler ah;
    public bool followPathing = false;
    [SerializeField]
    protected PathNode currentNode;
    protected Vector3 nextPoint;
    protected float radius;
    protected float theta;
    protected float step;

    protected int currFrame;
    public Dictionary<int, PathNode> allNodes;


    public Vector3 centerPoint;

    public enum PathType
    {
        CLOCK_CIRCLE = 0,
        COUNTER_CIRCLE = 1,
        LINE = 2,
        PAUSE = 3
    }






    [System.Serializable]
    public struct PathNode
    {
        public PathType type;
        public Vector3 pointOffset;
        public float timeToComplete;
    }

    [System.Serializable]
    public struct Path
    {
        public int[] frames;
        public PathNode[] pathNodes;
    }



    public void BeginPath(Path p)
    {
        allNodes.Clear();
        for(int i = 0; i < p.pathNodes.Length; i++)
        {
            allNodes.Add(p.frames[i], p.pathNodes[i]);
        }
        currFrame = 0;
        


    }



    public void BeginNewShape(PathNode p)
    {
        followPathing = true;

        currentNode = p;
        switch (p.type)
        {
            case (PathType.CLOCK_CIRCLE):
                BeginCircle(true);
                break;

            case (PathType.COUNTER_CIRCLE):
                BeginCircle(false);
                break;
            case (PathType.PAUSE):
                PausePathing();
                break;
            default:
                BeginLine();
                break;
        }
    }


    public void BeginLine()
    {
        step = currentNode.pointOffset.magnitude / currentNode.timeToComplete;
    }
    public void UpdateLine()
    {
        Vector3 velocity = (currentNode.pointOffset.normalized * step);
        if (anchorObject != null)
        {
            velocity += (Vector3)anchorObject.GetComponent<Rigidbody2D>().velocity;
        }
        rb2d.velocity = velocity;
    }

    public void BeginCircle(bool clockwise)
    {
        radius = currentNode.pointOffset.magnitude;
        theta = Mathf.Atan2(currentNode.pointOffset.y, currentNode.pointOffset.x);

        //Debug.Break();
        step = 2 * Mathf.PI / currentNode.timeToComplete;
        if (clockwise)
        {
            step *= -1;
        }
      

    }

    public void UpdateCircle()
    {
        centerPoint = transform.position - (radius * new Vector3(Mathf.Cos(theta), Mathf.Sin(theta)));
        
        
        theta += (step * Time.deltaTime);
        if (theta < 0)
        {
            theta += 2 * Mathf.PI;
        }

        if (theta >= 2 * Mathf.PI)
        {
            theta -= 2 * Mathf.PI;
       
        
        
        }
        nextPoint = centerPoint + (radius * new Vector3(Mathf.Cos(theta), Mathf.Sin(theta)));

        
        /*
        Vector3 temp = nextPoint;
        nextPoint = radiusPosition;
        radiusPosition = temp;
        */

        Vector3 velocity = (nextPoint - transform.position).normalized * radius * Mathf.Abs(step);
        if (anchorObject != null)
        {
            velocity += (Vector3)anchorObject.GetComponent<Rigidbody2D>().velocity;
        }
        rb2d.velocity =  velocity;
    }





    // Start is called before the first frame update
    void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
        ah = GetComponent<ActionHandler>();
        allNodes = new Dictionary<int, PathNode>();
    }


    private void FixedUpdate()
    {
        if (allNodes.Count != 0)
        {
            if (allNodes.TryGetValue(currFrame, out PathNode val))
            {
                BeginNewShape(val);
                allNodes.Remove(currFrame);
            }
            currFrame += 1;
        }
    }

    // Update is called once per frame
    void Update()
    {
        





        if (followPathing)
        {
            switch (currentNode.type)
            {
                case PathType.LINE:
                    UpdateLine();
                    break;
                case (PathType.CLOCK_CIRCLE):
                    UpdateCircle();
                    break;
                case (PathType.COUNTER_CIRCLE):
                    UpdateCircle();
                    break;
                default:
                    rb2d.velocity = Vector2.zero;
                    break;
            }
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            ah.PerformAction(pathingAction);
        }
        
    }

    public void PausePathing()
    {

        followPathing = false;
        rb2d.velocity = Vector2.zero;
        allNodes.Clear();
        currFrame = 0;
        

    }
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.white;
        Gizmos.DrawSphere(centerPoint, 1);

        Gizmos.color = Color.red;
        Gizmos.DrawSphere(nextPoint, 1);
    }
}
