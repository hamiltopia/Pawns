using System.Collections;
using System.Collections.Generic;
using UnityEngine.AI;
using UnityEngine;

public class Spawner : MonoBehaviour
{

    //Public Variables
    public GameObject prefabToSpawn;
    public float spawnTime;
    public float spawnTimeRandom;
    //Private Variables
    private float spawnTimer;

    private NavMeshAgent agent;


    //Used for initialisation
    void Start()
    {
        ResetSpawnTimer();
    }

    //Update is called once per frame
    void Update()
    {
        spawnTimer -= Time.deltaTime;
        if (spawnTimer <= 0.0f)
        {
            Instantiate(prefabToSpawn, transform.position, Quaternion.identity);
            ResetSpawnTimer();

            agent.SetDestination(RandomNavmeshLocation(4f));
        }
    }

    //Resets the spawn timer with a random offset
    void ResetSpawnTimer()
    {
        spawnTimer = (float)(spawnTime + Random.Range(0, spawnTimeRandom * 100) / 100.0);
    }

    //Random NavMesh Location
    public Vector3 RandomNavmeshLocation(float radius)
    {
        Vector3 randomDirection = Random.insideUnitSphere * radius;
        randomDirection += transform.position;
        NavMeshHit hit;
        Vector3 finalPosition = Vector3.zero;
        if (NavMesh.SamplePosition(randomDirection, out hit, radius, 1))
        {
            finalPosition = hit.position;
        }
        return finalPosition;
    }

}
