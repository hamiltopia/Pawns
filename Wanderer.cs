using System.Collections;
using System.Collections.Generic;
using UnityEngine.AI;
using UnityEngine;

public class Wanderer : MonoBehaviour
{
    public float chaseRadius = 20f;
    public float attackRadius = 2f;

    public float startSpeed;

    private NavMeshAgent agent;
    private GameObject player;

    private float timer;

    // Wander 
    public float wanderRadius = 30f;
    public float wanderTimer = 6f;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindWithTag("Player");
        agent = GetComponent<NavMeshAgent>();
        startSpeed = agent.speed;
        timer = wanderTimer;
    }

    // Update is called once per frame
    void Update()
    {
        // Look and See
        if (Vector3.Distance(this.transform.position, player.transform.position) <= chaseRadius)
        {
            agent.speed = startSpeed * 2f;
            agent.destination = player.transform.position;
            FaceTarget();
        }
        else if (Vector3.Distance(this.transform.position, player.transform.position) > chaseRadius)//Back to Patrol
        {
            timer += Time.deltaTime;
            agent.speed = startSpeed;

            if (timer >= wanderTimer)
            {
                Vector3 newPos = RandomNavSphere(transform.position, wanderRadius, -1);
                agent.SetDestination(newPos);
                timer = 0;
            }
        }

        if (Vector3.Distance(this.transform.position, player.transform.position) <= attackRadius)
        {
            FaceTarget();
            Debug.Log("Attack!!!");
        }
    }

    void FaceTarget()
    {
        Vector3 direction = (player.transform.position - transform.position).normalized;
        Quaternion lookRotation = Quaternion.LookRotation(new Vector3(direction.x, 0, direction.z));
        transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * 5f);
    }

    public static Vector3 RandomNavSphere(Vector3 origin, float dist, int layermask)
    {
        Vector3 randDirection = Random.insideUnitSphere * dist;
        randDirection += origin;
        NavMeshHit navHit;
        NavMesh.SamplePosition(randDirection, out navHit, dist, layermask);
        return navHit.position;
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, chaseRadius);

        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, attackRadius);

        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(transform.position, wanderRadius);
    }
}
