using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class DrawNavMeshPath : MonoBehaviour
{
    public static Vector3[] path = new Vector3[0];

    private LineRenderer lr;

    private NavMeshAgent agent;

    // Start is called before the first frame update
    void Start()
    {
        lr = GetComponent<LineRenderer>();
        agent = GetComponent<NavMeshAgent>();
    }

    // Update is called once per frame
    void Update()
    {
        DrawNavMeshPath.path = agent.path.corners;
        if (path != null && path.Length > 1)
        {
            lr.positionCount = path.Length;
            for (int i = 0; i < path.Length; i++)
            {
                lr.SetPosition(i, path[i]);
            }
        }
    }
}
