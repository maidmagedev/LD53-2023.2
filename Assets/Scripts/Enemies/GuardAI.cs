using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class GuardAI : MonoBehaviour
{
    private NavMeshAgent agent;
    private GameObject target;
    private DetectionCone detectionCone;
    [SerializeField] private List<GameObject> wayPoints = new();

    private bool patrolling = false;
    private int currWaypointIndex = 0;
    // Start is called before the first frame update
    void Start()
    {
        detectionCone = GetComponentInChildren<DetectionCone>();
        agent = GetComponent<NavMeshAgent>();
        agent.updateRotation = false;
        target = GameObject.FindWithTag("Player"); 
        //print(target);
        agent.destination = wayPoints[0].transform.position;
        
    }

    // Update is called once per frame
    void Update()
    {
        if (detectionCone.get_touchingPlayer())
        {
            patrolling = false;
            StopAllCoroutines();
            agent.destination = Vector3.Lerp(this.transform.position, target.transform.position, .1f);
        }
        else
        {
            patrolling = true;
            Patrol();
        }

    }
    
    private void Patrol()
    {
        patrolling = true;
        if (agent.remainingDistance < 1f)
        {
            print(currWaypointIndex);
            ++currWaypointIndex;
            if (currWaypointIndex >= wayPoints.Count)
            {
                currWaypointIndex = 0;
            }
        }
        agent.destination = wayPoints[currWaypointIndex].transform.position;
    }

}
