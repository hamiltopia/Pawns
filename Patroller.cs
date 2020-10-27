using System.Collections;
using System.Collections.Generic;
using UnityEngine.AI;
using UnityEngine;

public class Patroller : MonoBehaviour
{
    public float chaseRadius = 20f;
    public float attackRadius = 2f;

    [SerializeField]
    private GameObject[] wayPoints;

    private NavMeshAgent agent;
    private GameObject player;
    private int currentPoint;
    private float startSpeed;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindWithTag("Player");
        agent = GetComponent<NavMeshAgent>();
        agent.autoBraking = false;
        startSpeed = agent.speed;
        currentPoint = 0;
        agent.destination = wayPoints[currentPoint].transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        
        //Chase Player
        if (Vector3.Distance(this.transform.position, player.transform.position) < chaseRadius)
        {
            agent.speed = startSpeed * 2f;
            agent.destination = player.transform.position;
            FaceTarget();
        }
        else if (Vector3.Distance(this.transform.position, player.transform.position) > chaseRadius)//Back to Patrol
        {
            agent.speed = startSpeed;
            agent.destination = wayPoints[currentPoint].transform.position;//current waypoint
        }

        //Attack
        if (Vector3.Distance(this.transform.position, player.transform.position) < attackRadius)
        {
            AttackPlayer();
            FaceTarget();
            Debug.Log("Attack");
        }

        //Go back to Waypoints
        if (Vector3.Distance(this.transform.position, wayPoints[currentPoint].transform.position) < attackRadius)
        {
            Iterate();
        }
        

    }

    //Cycle through Waypoints
    void Iterate()
    {
        if (currentPoint < wayPoints.Length - 1)
        {
            currentPoint++;
        }
        else
        {
            currentPoint = 0;
        }

        agent.destination = wayPoints[currentPoint].transform.position;
    }

    void FaceTarget()
    {
        Vector3 direction = (player.transform.position - transform.position).normalized;
        Quaternion lookRotation = Quaternion.LookRotation(new Vector3(direction.x, 0, direction.z));
        transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * 5f);
    }

    void AttackPlayer()
    {
        agent.destination = player.transform.position;
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, chaseRadius);

        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, attackRadius);
    }
}
