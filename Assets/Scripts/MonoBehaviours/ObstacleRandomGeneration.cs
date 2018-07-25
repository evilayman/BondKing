using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleRandomGeneration : MonoBehaviour
{

    [SerializeField]
    private Vector3 randomLimit;
    private Vector3 spawnLocation, ObstacleDir;
    [SerializeField]
    private List<GameObject> Obstacles;
    [SerializeField]
    private GameObject player;
    [SerializeField]
    private float SpawnTime,destroyTime, followPlayerSpeed;
    private int objectToSpawn;

    private void Start()
    {
        InvokeRepeating("SpawnObstacles", 0, SpawnTime);
    }
    void Update()
    {

    }

    void SpawnObstacles()
    {
        var rand1 = Random.Range(-randomLimit.x, randomLimit.x);
        spawnLocation.x = rand1;
        spawnLocation.y = Random.Range(0, randomLimit.y);


        //rand1 = Random.Range(-randomLimit.z, -randomLimit.z + 25);
        //var rand2 = Random.Range(randomLimit.z - 25, randomLimit.z);
        var randRes = Random.Range(0, 2);
        if (randRes == 0)
            spawnLocation.z = randomLimit.z;
        else
            spawnLocation.z = -randomLimit.z;

        objectToSpawn = Random.Range(0, Obstacles.Count);
        GameObject instantiatedObstacle = Instantiate(Obstacles[objectToSpawn], spawnLocation, Quaternion.identity);
        ObstacleDir = player.transform.position - instantiatedObstacle.transform.position;
        instantiatedObstacle.GetComponent<Rigidbody>().AddForce(ObstacleDir * followPlayerSpeed);
        Destroy(instantiatedObstacle, destroyTime);


    }
}
